using System;
using System.Collections.Generic;

namespace Eco.Mods.FiniteOil
{
    using Core.Plugins.Interfaces;
    using Core.Utils;
    using Core.Plugins;

    using Shared.Localization;
    using Shared.Utils;
    using Shared.Math;

    using Simulation.WorldLayers;

    using Gameplay.GameActions;
    using Gameplay.Objects;
    using Gameplay.Aliases;
    using Gameplay.Property;

    [Localized]
    public class FiniteOilConfig
    {
        [LocDescription("How much to decay the oil field as a % per barrel of petroleum extracted.")]
        public float ExtractRate { get; set; } = 0.01f / 100.0f;
    }

    public class OilExtractionPuller : AccumulatingPuller.IAccumulatingPullerDocumenter
    {
        public string DescribeGeneral(AccumulatingPuller puller) => "Records the oil extracted by pumpjacks since last tick.";

        public string DescribeAggregated(AccumulatingPuller puller, float average) => $"An average of {average:g2} oil per square meter was extracted since last tick.";
    }

    [Localized]
    public class NoopInteraction : WorldLayerInteraction<float>
    {
        public override string[] DependencyLayerNames => new string[0];

        public override string[] WorldInitDependencyLayerNames => new string[0];

        public override string OutputLayerName => layerName;

        public override string DescribeGeneral => string.Empty;

        private readonly string layerName;

        public NoopInteraction(string layerName)
        {
            this.layerName = layerName;
        }

        public override float Apply(float currentValue, float[] dependencyValues, WorldLayerNeighborInfo[] neighborValues)
            => 0.0f;

        public override float PostWorldgen(float currentValue, float[] dependencyValues, WorldLayerNeighborInfo[] neighborValues) => this.Apply(currentValue, dependencyValues, neighborValues);

        protected override string DescribeAggregated(IEnumerable<float> intermediateDescriptions)
            => string.Empty;

        protected override float DescribeSpecific(float currentValue, float[] dependencyValues, WorldLayerNeighborInfo[] neighborValues)
            => 0.0f;
    }

    [Localized, LocDisplayName(nameof(FiniteOilPlugin)), Priority(PriorityAttribute.High)]
    public class FiniteOilPlugin : Singleton<FiniteOilPlugin>, IModKitPlugin, IConfigurablePlugin, IGameActionAware
    {
        public IPluginConfig PluginConfig => config;

        private PluginConfig<FiniteOilConfig> config;
        public FiniteOilConfig Config => config.Config;

        private readonly NoopInteraction noopInteraction;
        private readonly AccumulatingPuller oilExtraction;

        public FiniteOilPlugin()
        {
            config = new PluginConfig<FiniteOilConfig>("FiniteOil");
            ActionUtil.AddListener(this);
            this.noopInteraction = new NoopInteraction(LayerNames.Oilfield);
            this.oilExtraction = new AccumulatingPuller(LayerNames.Oilfield, new OilExtractionPuller());
            NewWorldLayerSync.AddInteraction(this.noopInteraction);
            NewWorldLayerSync.AddPuller(this.oilExtraction);
            // Note: the noop interaction is needed to switch the layer puller mode into "additive", otherwise the layer just gets reset to 0
        }

        public object GetEditObject() => Config;
        public ThreadSafeAction<object, string> ParamChanged { get; set; } = new ThreadSafeAction<object, string>();

        public void OnEditObjectChanged(object o, string param)
        {
            this.SaveConfig();
        }

        public string GetCategory() => Localizer.DoStr("Config");
        public string GetStatus() => string.Empty;
        public override string ToString() => Localizer.DoStr("FiniteOil");

        public LazyResult ShouldOverrideAuth(IAlias alias, IOwned property, GameAction action)
            => LazyResult.FailedNoMessage;

        public void ActionPerformed(GameAction action)
        {
            if (action is not ItemCraftedAction itemCraftedAction) { return; }
            if (itemCraftedAction.WorldObject == null) { return; }
            if (itemCraftedAction.WorldObject is not WorldObject worldObject) { return; }
            if (worldObject.GetType().Name != "PumpJackObject") { return; }
            var layer = WorldLayerManager.Obj.GetLayer(LayerNames.Oilfield);
            var layerCoord = layer.WorldPosToLayerPos(new Vector2i(itemCraftedAction.ActionLocation.X, itemCraftedAction.ActionLocation.Z));
            var currentAmount = layer[layerCoord];
            var extractAmount = currentAmount * Config.ExtractRate;
            oilExtraction.AddAmount(itemCraftedAction.ActionLocation, -extractAmount);
        }
    }
}
# Eco Finite Oil
A server mod for Eco 9.6 that causes oil fields to be depleted over time when worked.

## Installation
1. Download `EcoFiniteOilMod.dll` from the [latest release](https://github.com/thomasfn/EcoFiniteOilMod/releases).
2. Copy the `EcoFiniteOilMod.dll` file to `Mods` folder of the dedicated server.
3. Restart the server.

## Usage

The mod will begin working automatically when installed. It can safely be added to existing saves or removed at any time. Every time a pumpjack crafts one item, usually a barrel of Petroleum (unless you have modded recipes), a deduction will be applied to the 'Oilfield' world layer. The deduction is proportion to the current value of the oilfield, that is, a high density of oilfield will be used up faster. The deduction percentage is configurable.

## Config

There is a single config property called "ExtractRate", which is a percentage in fraction form (e.g. 0% = 0, 100% = 1, 50% = 0.5 etc). By default it is set to 0.0001 (0.01%). You can change it from the "FiniteOil" tab in the server UI, or by creating a file called "FiniteOil.eco" inside of the server's "Configs" folder and populating it with the following:

```
{
  "ExtractRate": 0.0001
}
```

To help you tweak this value, consider the following example case:

- Oil driller has 10 pumpjacks placed across 4 plots which start at 90% oilfield density each
- At 90% density the craft time for a barrel is roughly 1m 40s, which is about 36 crafts per hour
- After an hour of runtime at the default extract rate, each pumpjack would cause a decay of about 0.36% density
- Given that the oilfield world layer is per-plot and not per-block, at around 2.5 pumpjacks per plot, the oilfield density for all 4 plots would drop to 89.1% after hour
- After 1 day of continuous operation, they would be at 82.5% density each

Note that this isn't entirely accurate as the pumpjacks will slow down as the oilfield density drops. There's probably a proper mathsy way of compensating for this and building a formula that allows you to plug in roughly how much you would expect an oilfield to decay every 24 hrs given a particular oil rig setup.

## Building Mod from Source

### Windows

1. Login to the [Eco Website](https://play.eco/) and download the latest modkit
2. Extract the modkit and copy the dlls from `ReferenceAssemblies` to `eco-dlls` in the root directory (create the folder if it doesn't exist)
3. Open `EcoFiniteOilMod.sln` in Visual Studio 2019
4. Build the `EcoFiniteOilMod` project in Visual Studio
5. Find the artifact in `EcoFiniteOilMod\bin\{Debug|Release}\net6.0`

### Linux

1. Run `ECO_BRANCH="release" MODKIT_VERSION="0.9.6.0-beta" fetch-eco-reference-assemblies.sh` (change the modkit branch and version as needed)
2. Enter the `EcoFiniteOilMod` directory and run:
`dotnet restore`
`dotnet build`
3. Find the artifact in `EcoFiniteOilMod/bin/{Debug|Release}/net6.0`

## License
[MIT](https://choosealicense.com/licenses/mit/)
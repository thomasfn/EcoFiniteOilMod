# Eco Finite Oil
A server mod for Eco 9.5 that causes oil fields to be depleted over time when worked.

## Installation
1. Download `EcoFiniteOilMod.dll` from the [latest release](https://github.com/thomasfn/EcoFiniteOilMod/releases).
2. Copy the `EcoFiniteOilMod.dll` file to `Mods` folder of the dedicated server.
3. Restart the server.

## Usage

TODO: Notes about config

## Building Mod from Source

### Windows

1. Login to the [Eco Website](https://play.eco/) and download the latest modkit
2. Extract the modkit and copy the dlls from `ReferenceAssemblies` to `eco-dlls` in the root directory (create the folder if it doesn't exist)
3. Open `EcoFiniteOilMod.sln` in Visual Studio 2019
4. Build the `EcoFiniteOilMod` project in Visual Studio
5. Find the artifact in `EcoFiniteOilMod\bin\{Debug|Release}\net5.0`

### Linux

1. Run `ECO_BRANCH="staging" MODKIT_VERSION="0.9.5.0-beta-staging-2230" fetch-eco-reference-assemblies.sh` (change the modkit branch and version as needed)
2. Enter the `EcoFiniteOilMod` directory and run:
`dotnet restore`
`dotnet build`
3. Find the artifact in `EcoFiniteOilMod/bin/{Debug|Release}/net5.0`

## License
[MIT](https://choosealicense.com/licenses/mit/)
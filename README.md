# RTS Engine Module: Advanced Coloured Renderer
A handy Tool for assigning a parent or a list of mesh renderers to assign to the faction coloured renderer within the component, it also includes an Editor script for the extra interface and tooling options.
This Component replaces the default Coloured Renderer Component on an Entity

A shader sample has also been provided, this uses a mask map texture for Roughness/Metalic/AO and uses a black/white texture mask for faction colour, the FactionColour property can be mapped to in the component, in case some materials need 2 properties setting an option to include multiple properties has been added.

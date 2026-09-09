using MudBlazor;

namespace Airen.Web.Themes;

public static class AirenTheme {
    
    public static MudTheme Default => new () {

        PaletteLight = new PaletteLight {

			Primary = "#43A5D2",
            Secondary = "#8C8500",

            Background = "#F7FAFC",
            Surface = "#FFFFFF",
            
            AppbarBackground = "#F7FAFC",
            AppbarText = "#263238",
             
            DrawerBackground = "#FFFFFF",
            DrawerText = "#37474F",
            
            TextPrimary = "#263238",
            TextSecondary = "#607D8B"

	    },

        PaletteDark = new PaletteDark {

            Primary = "#43A5D2",
            Secondary = "#B4AD22",
            
            Background = "#101820",
            Surface = "#1C2B34",

            AppbarBackground = "#101820",
            AppbarText = "#FFFFFF",
             
            DrawerBackground = "#101820",
            DrawerText = "#FFFFFF",
            
            TextPrimary = "#FFFFFF", 
            TextSecondary = "#B0BEC5"

        }
    };

}
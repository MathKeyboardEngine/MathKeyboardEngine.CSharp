using Microsoft.JSInterop;

namespace BlazorWebAssemblyAppWithKatex;

public class ColorScheme
{
    public async Task LoadOSPreferenceAsync(IJSRuntime js)
    {
        var script = await js.InvokeAsync<IJSObjectReference>("import", "/colorScheme.js");
        var prefersLightMode = await script.InvokeAsync<bool>("prefersLightMode");
        Preference = prefersLightMode ? PreferenceType.Light : PreferenceType.Dark;
    }

    public void Toggle()
    {
        Preference = PreferenceType.Light == Preference ? PreferenceType.Dark : PreferenceType.Light;
    }

    public PreferenceType Preference { get; set; }
    public enum PreferenceType { Light, Dark }

    public string SelectionModeColor => Preference == PreferenceType.Light ? "#add8e6" : "#1668c7";
}

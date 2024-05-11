using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace client.Infrastructure;

public static class ChartHelpers
{
    public static List<SKColor> ChartColors { get; } =
    [
        Colors.Aqua.ToSKColor(),
        Colors.Red.ToSKColor(),
        Colors.Blue.ToSKColor(),
        Colors.Green.ToSKColor(),
        Colors.Brown.ToSKColor(),
        Colors.Chartreuse.ToSKColor(),
        Colors.Chocolate.ToSKColor(),
        Colors.Fuchsia.ToSKColor(),
        Colors.Gold.ToSKColor(),
        Colors.Indigo.ToSKColor(),
        Colors.Magenta.ToSKColor(),
        Colors.Moccasin.ToSKColor(),
        Colors.Olive.ToSKColor(),
        Colors.Navy.ToSKColor(),
        Colors.Sienna.ToSKColor(),
        Colors.Teal.ToSKColor(),
        Colors.Tomato.ToSKColor(),
        Colors.CadetBlue.ToSKColor(),
        Colors.DarkKhaki.ToSKColor(),
        Colors.MistyRose.ToSKColor()
    ];
}
using System.Linq;
using UnityEngine;

namespace EmikBaseModules
{
    public static class ColorHelper
    {
        /// <summary>
        /// Modifies an existing color.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments replacing the values of the color.</returns>
        internal static Color32 Replace(this Color32 color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color32(
                r == null ? (byte)(r * 255) : color.r,
                g == null ? (byte)(g * 255) : color.g,
                b == null ? (byte)(b * 255) : color.b,
                a == null ? (byte)(a * 255) : color.a);
        }

        /// <summary>
        /// Modifies an existing color.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments replacing the values of the color.</returns>
        internal static Color32 Replace(this Color32 color, byte? r = null, byte? g = null, byte? b = null, byte? a = null)
        {
            return new Color32(
                r == null ? (byte)r : color.r,
                g == null ? (byte)g : color.g,
                b == null ? (byte)b : color.b,
                a == null ? (byte)a : color.a);
        }

        /// <summary>
        /// Modifies an existing color.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments replacing the values of the color.</returns>
        internal static Color Replace(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(
                r == null ? (float)r : color.r,
                g == null ? (float)g : color.g,
                b == null ? (float)b : color.b,
                a == null ? (float)a : color.a);
        }

        /// <summary>
        /// Modifies an existing color by replacing each component.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments adding the values of the color.</returns>
        internal static Color Replace(this Color color, byte? r = null, byte? g = null, byte? b = null, byte? a = null)
        {
            return new Color(
                r == null ? (float)r / 255 : color.r,
                g == null ? (float)g / 255 : color.g,
                b == null ? (float)b / 255 : color.b,
                a == null ? (float)a / 255 : color.a);
        }

        /// <summary>
        /// Modifies an existing color by adding each component.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments replacing the values of the color.</returns>
        internal static Color32 Add(this Color32 color, float r = 0, float g = 0, float b = 0, float a = 0)
        {
            return new Color32(
                (byte)((r * 255) + color.r),
                (byte)((g * 255) + color.g),
                (byte)((b * 255) + color.b),
                (byte)((a * 255) + color.a));
        }

        /// <summary>
        /// Modifies an existing color by adding each component.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments adding the values of the color.</returns>
        internal static Color32 Add(this Color32 color, byte r = 0, byte g = 0, byte b = 0, byte a = 0)
        {
            return new Color32(
                (byte)(r + color.r),
                (byte)(g + color.g),
                (byte)(b + color.b),
                (byte)(a + color.a));
        }

        /// <summary>
        /// Modifies an existing color by adding each component.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments adding the values of the color.</returns>
        internal static Color Add(this Color color, float r = 0, float g = 0, float b = 0, float a = 0)
        {
            return new Color(
                r + color.r,
                g + color.g,
                b + color.b,
                a + color.a);
        }

        /// <summary>
        /// Modifies an existing color by adding each component.
        /// </summary>
        /// <param name="color">The color to modify.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>A new instance of the color, with the arguments adding the values of the color.</returns>
        internal static Color Add(this Color color, byte r = 0, byte g = 0, byte b = 0, byte a = 0)
        {
            return new Color(
                (r / 255) + color.r,
                (g / 255) + color.g,
                (b / 255) + color.b,
                (a / 255) + color.a);
        }

        /// <summary>
        /// Returns true/false if the array has any color that matches the same RGBA components as the other color.
        /// </summary>
        /// <param name="a">The array to scan through and check for equality.</param>
        /// <param name="b">The color to compare equality with.</param>
        /// <returns>True if any in the array match the other color's r, g, b, and a fields.</returns>
        internal static bool IsAnyEqual(this Color32[] a, Color32 b)
        {
            return a.Any(c => c.Equals(b));
        }

        /// <summary>
        /// Returns true/false if the array has any color that matches the same RGBA components as the other color.
        /// </summary>
        /// <param name="a">The array to scan through and check for equality.</param>
        /// <param name="b">The color to compare equality with.</param>
        /// <returns>True if any in the array match the other color's r, g, b, and a fields.</returns>
        internal static bool IsAnyEqual(this Color[] a, Color b)
        {
            return a.Any(c => c.Equals(b));
        }

        /// <summary>
        /// Returns true/false if the colors have the same RGBA components.
        /// </summary>
        /// <param name="a">The first color to compare equality with.</param>
        /// <param name="b">The second color to compare equality with.</param>
        /// <returns>True if both color's r, g, b, and a fields are equal.</returns>
        internal static bool IsEqual(this Color32 a, Color32 b)
        {
            return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
        }

        /// <summary>
        /// Returns true/false if the colors have the same RGBA components.
        /// </summary>
        /// <param name="a">The first color to compare equality with.</param>
        /// <param name="b">The second color to compare equality with.</param>
        /// <returns>True if both color's r, g, b, and a fields are equal.</returns>
        internal static bool IsEqual(this Color a, Color b)
        {
            return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
        }

        /// <summary>
        /// Mixes the two colors provided and sets the renderer.material.color to be that color. Weighting can be included.
        /// </summary>
        /// <param name="renderer">The renderer to change color. This does mean that the renderer's material must support color.</param>
        /// <param name="colorA">The first color, as f approaches 0.</param>
        /// <param name="colorB">The second color, as f approaches 1.</param>
        /// <param name="f">The weighting of color mixing, with 0 being 100% colorA and 1 being 100% colorB.</param>
        internal static void SetIntertwinedColor(this Renderer renderer, Color32 colorA, Color32 colorB, float f = 0.5f)
        {
            float negF = 1 - f;
            renderer.material.color = new Color32((byte)((colorA.r * negF) + (colorB.r * f)), (byte)((colorA.g * negF) + (colorB.g * f)), (byte)((colorA.b * negF) + (colorB.b * f)), (byte)((colorA.a * negF) + (colorB.a * f)));
        }

        /// <summary>
        /// Mixes the two colors provided and sets the renderer.material.color to be that color. Weighting can be included.
        /// </summary>
        /// <param name="renderer">The renderer to change color. This does mean that the renderer's material must support color.</param>
        /// <param name="colorA">The first color, as f approaches 0.</param>
        /// <param name="colorB">The second color, as f approaches 1.</param>
        /// <param name="f">The weighting of color mixing, with 0 being 100% colorA and 1 being 100% colorB.</param>
        internal static void SetIntertwinedColor(this Renderer renderer, Color colorA, Color colorB, float f = 0.5f)
        {
            float negF = 1 - f;
            renderer.material.color = new Color((colorA.r * negF) + (colorB.r * f), (colorA.g * negF) + (colorB.g * f), (colorA.b * negF) + (colorB.b * f), (colorA.a * negF) + (colorB.a * f));
        }
    }
}

# ImageBuilder

[All docs](../index.md)

<a name="imagebuilder"></a>

## ImageBuilder

*class*

A picture drawn in code, pixel by pixel or with simple shapes, then [`ModImages.Register()`](ModImages.md#modimages-register)ed and used anywhere a mod's image file goes: an item's or tool's icon, a material's texture, a UI image.

```csharp
var image = new ImageBuilder(32, 32, new Color32(40, 40, 40, 255));
image.FillCircle(16, 16, 10, new Color32(255, 200, 0, 255));
string icon = Images.Register("coin", image);   // "image:coin"
```

(0, 0) is the top-left pixel; x goes right, y down. Drawing off the edges is clipped. At most 1024 by 1024.

### Constructors

<a name="imagebuilder-new"></a>

#### new ImageBuilder

```csharp
ImageBuilder(int width, int height, Color32 fill = default(Color32))
```

A new image, all `fill` (clear if not given).

### Fields

<a name="imagebuilder-maxsize"></a>

#### MaxSize

```csharp
const int MaxSize = 1024
```

### Properties

<a name="imagebuilder-width"></a>

#### Width

```csharp
int Width { get; }
```

<a name="imagebuilder-height"></a>

#### Height

```csharp
int Height { get; }
```

### Methods

<a name="imagebuilder-getpixel"></a>

#### GetPixel

```csharp
Color32 GetPixel(int x, int y)
```

The pixel's color, or clear off the edges.

<a name="imagebuilder-setpixel"></a>

#### SetPixel

```csharp
void SetPixel(int x, int y, Color32 color)
```

<a name="imagebuilder-blendpixel"></a>

#### BlendPixel

```csharp
void BlendPixel(int x, int y, Color color)
```

Paints over a pixel by the color's alpha, as a brush would.

<a name="imagebuilder-fill"></a>

#### Fill

```csharp
void Fill(Color32 color)
```

Sets every pixel.

<a name="imagebuilder-fillrect"></a>

#### FillRect

```csharp
void FillRect(int x, int y, int width, int height, Color32 color)
```

<a name="imagebuilder-drawrect"></a>

#### DrawRect

```csharp
void DrawRect(int x, int y, int width, int height, Color32 color)
```

A one-pixel outline of a rectangle.

<a name="imagebuilder-drawline"></a>

#### DrawLine

```csharp
void DrawLine(int x0, int y0, int x1, int y1, Color32 color)
```

A one-pixel line between two pixels, both ends included.

<a name="imagebuilder-fillcircle"></a>

#### FillCircle

```csharp
void FillCircle(int cx, int cy, int radius, Color32 color)
```

A filled circle round a pixel.

<a name="imagebuilder-drawcircle"></a>

#### DrawCircle

```csharp
void DrawCircle(int cx, int cy, int radius, Color32 color)
```

A one-pixel circle outline round a pixel.

<a name="imagebuilder-drawimage"></a>

#### DrawImage

```csharp
void DrawImage(ImageBuilder image, int x, int y, bool blend = true)
```

Draws another image with its top-left at (x, y), blending by its alpha (or copying it exactly with `blend` false).

<a name="imagebuilder-copy"></a>

#### Copy

```csharp
ImageBuilder Copy()
```

A copy to draw on without changing this one.

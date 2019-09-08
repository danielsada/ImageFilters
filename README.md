# ImageFilters

## Color Space Background

### CMYK Color Representation

When we see a printed advertisement or poster, the colours are printed with colour spaces based on the [CMYK colour model](https://en.wikipedia.org/wiki/CMYK_color_model), using the subtractive primary colours of pigment **C**yan, **M**agenta, **Y**ellow, and blac**K**. This is also known as the four-colours print process.

![Offset Printing](https://cdn.filestackcontent.com/DbR1Q6LTkiuoJkuJgDUV "Offset Printing")

The "primary" and "secondary" colours in a four-colour print process are exhibited below.

![CMYK Colors](https://cdn.filestackcontent.com/3wzNOfsfTMqscjBavlq9 "CMYK Colors")

### RGB Color Representation

While printed colours are represented with the use of the four-colour process, monitors represent colour using the [RGB colour model](https://en.wikipedia.org/wiki/RGB_color_model) which is an additive colour model in which **R**ed, **G**reen and **B**lue light are added together in various ways to reproduce a broad array of colours.

![Computer Monitor](https://cdn.filestackcontent.com/DhwGuKL8QySONcBTRDe7 "Computer Monitor")

### The Difference Between Print and Monitor Color

[Offset lithography](https://en.wikipedia.org/wiki/Offset_printing) is one of the most common ways of creating printed materials. A few of its applications include newspapers, magazines, brochures, stationery, and books. This model requires the image to be converted or made in the CMYK colour model. All printed material relies on creating pigments of colour that combined forms the colour, as shown below.

![Offset Print Example](https://cdn.filestackcontent.com/mXHi44pSTl6xAZsNHnRi "Offset Print Example")

The ink semi-opacity property in conjunction with the halftone technology is responsible for allowing pigments to mix and create new colours with just four primary ones.

On the other hand, media that transmit light (such as the monitor on your PC, Tablet, or Phone) use [additive colour mixing](https://en.wikipedia.org/wiki/Additive_color), which means that every pixel is made from three colours (RGB colour model) by displaying different intensity the colours get produced.

## Workspace Setup

### build and install the Mono libgdiplus.dll

Depending on your system installation of `libgdiplus` will differ, on a Mac you can do this by executing the following commands,

```bash
# First install XQuartz, then...
brew install autoconf libtool automake
brew install freetype fontconfig
brew install libtool
brew install glib
brew install cairo
brew install libpng12

# Download from https://github.com/mono/libgdiplus
cd libgdiplus
CPPFLAGS="-I/usr/local/opt/libpng12/include -I/opt/X11/include" LDFLAGS="-L/usr/local/opt/libpng12/lib -L/usr/X11/lib" ./autogen.sh
make install
```

### VS Code Setup

The project was build using [Visual Studio Code](https://code.visualstudio.com/); you will need `C# for VS Code` and `Visual Studio IntelliCode`.

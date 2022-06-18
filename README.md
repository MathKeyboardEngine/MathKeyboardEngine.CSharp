## MathKeyboardEngine for C#

MathKeyboardEngine provides the logic - in C# and LaTeX - for a highly customizable virtual math keyboard. It is intended for use together with any LaTeX typesetting library.

## Which LaTeX typesetting libraries?

- If you're making a [Blazor WebAssembly app](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor), then use for example [KaTeX](https://katex.org/) or [MathJax](https://www.mathjax.org/). You have the keyboard-logic from MathKeyboardEngine in C# and an extreme amount of LaTeX commands after just a few lines of JavaScript glue code - examples will follow.
- If you're making a Windows desktop application, then use for example [WPF-Math](https://github.com/ForNeVeR/wpf-math). Note that MathKeyboardEngine has its own syntax tree and that it works independently.
- In all other cases, use for example [CSharpMath](https://github.com/verybadcat/CSharpMath). CSharpMath can render LaTeX even for mobile development. Note that CSharpMath distributes several packages and that it also has its own keyboard functionality - however, you may want to choose MathKeyboardEngine for its flexibility.

Note: port in progress (from [MathKeyboardEngine for JavaScript](https://github.com/MathKeyboardEngine/MathKeyboardEngine)).
<<<<<<< HEAD
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Maui.eCommerce
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
=======
using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Maui.eCommerce;

class Program : MauiApplication
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    static void Main(string[] args)
    {
        var app = new Program();
        app.Run(args);
    }
}
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9

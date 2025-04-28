<<<<<<< HEAD
﻿namespace Maui.eCommerce
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
=======
﻿namespace Maui.eCommerce;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9

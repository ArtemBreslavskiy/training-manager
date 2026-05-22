using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TrainingManager.Views;

public partial class TrainingProgramPageView : UserControl
{
    public TrainingProgramPageView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
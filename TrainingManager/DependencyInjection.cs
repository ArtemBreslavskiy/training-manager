using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainingManager.Data;
using TrainingManager.Services;
using TrainingManager.Utils;
using TrainingManager.ViewModels;

namespace TrainingManager;

internal static class DependencyInjection
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=training";

    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddDbContextFactory<TrainingContext>(options =>
            options.UseNpgsql(ConnectionString));

        services.AddSingleton<TrainingProgramService>();
        services.AddSingleton<ExerciseService>();
        services.AddSingleton<WorkoutService>();
        services.AddSingleton<PagesUtils>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<WelcomePageViewModel>();
        services.AddTransient<TrainingProgramPageViewModel>();
        services.AddTransient<ChartsPageViewModel>();

        return services.BuildServiceProvider();
    }
}

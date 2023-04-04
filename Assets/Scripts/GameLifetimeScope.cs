using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MainUI _mainUI;
    [SerializeField] private LineClick _lineClick;
    [SerializeField] private TargetCheck _targetCheck;
    [SerializeField] private FigureChecker _figureChecker;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.UseEntryPoints( entryPoints =>
        {
            entryPoints.Add<GameManager>();
            entryPoints.Add<RestartScene>();
        });
        
        builder.RegisterComponent(_mainUI);
        builder.RegisterComponent(_lineClick);
        builder.RegisterComponent(_targetCheck);
        builder.RegisterComponent(_figureChecker);
    }
}

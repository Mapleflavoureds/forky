using Content.Shared.Silicons.Borgs;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;

namespace Content.Client.Silicons.Borgs;

[UsedImplicitly]
public sealed class IPCBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    private IPCMenu? _menu;

    public IPCBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _menu = this.CreateWindow<IPCMenu>();
        _menu.SetEntity(Owner);

        _menu.EjectBatteryButtonPressed += () =>
        {
            SendMessage(new BorgEjectBatteryBuiMessage());
        };

        _menu.RemoveModuleButtonPressed += module =>
        {
            SendMessage(new BorgRemoveModuleBuiMessage(EntMan.GetNetEntity(module)));
        };
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (state is not BorgBuiState msg)
            return;
        _menu?.UpdateState(msg);
    }
}

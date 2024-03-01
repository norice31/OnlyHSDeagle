using System;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;


namespace OnlyHSDeagle;


public class OnlyHSDeagle : BasePlugin
{
    public override string ModuleAuthor => "nomixe";
    public override string ModuleName => "OnlyHSDeagle";
    public override string ModuleVersion => "v1";

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt, HookMode.Pre);
    }

    private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {
        if (!@event.Userid.IsValid)
        {
            return HookResult.Continue;
        }

        CCSPlayerController player = @event.Userid;

        if (player.Connected != PlayerConnectedState.PlayerConnected)
        {
            return HookResult.Continue;
        }

        if (!player.PlayerPawn.IsValid)
        {
            return HookResult.Continue;
        }

        if (@event.Weapon == "deagle")
        {
            if (@event.Hitgroup != 1) {
                if (@event.Userid.PlayerPawn.Value.Health + @event.DmgHealth <= 100)
                {
                    @event.Userid.PlayerPawn.Value.Health = @event.Userid.PlayerPawn.Value.Health + @event.DmgHealth;
                } else
                {
                    @event.Userid.PlayerPawn.Value.Health = 100;
                }
            }
        }

        @event.Userid.PlayerPawn.Value.VelocityModifier = 1;

        return HookResult.Continue;
    }
}
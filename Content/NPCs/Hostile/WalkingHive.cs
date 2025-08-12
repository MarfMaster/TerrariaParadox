using Terraria.ModLoader;

namespace TerrariaParadox.Content.NPCs.Hostile;
[Autoload(false)]
public class WalkingHive : ModNPC
{
    public const int Width = 34;
    public const int Height = 48;
    public const int BaseDmg = 15;
    public const int Defense = 0;
    public const int DefenseFromBack = 20;
    public const int BaseHP = 250;
    public const int Value = 100;
    public const int FrameDuration = 5;
    private enum ActionState
    {
        Wander,
        Notice,
        Preparing,
        Puke,
        Tired,
        Fleeing,
        Explode
    }
    /// <summary>
    /// Notes:
    /// Wanders about in a random direction, until he notices the player.
    /// Once he notices the player, he will first turn towards them for half a second and then turn his back to the player for about 2 secs and prepare to puke.
    /// Once the prep is done, he will face the player to puke at them.
    /// After having puked, he will keep facing that direction for a few secs and be tired.
    /// If the player can't finish it off while it's tired, it'll then turn it's back to the player and try to run away for a few secs.
    /// Then, it'll go back to preparing to puke if a nearby player is still alive. Else it'll go back to wandering.
    /// Every time this enemy is hit in the back, it'll gain a stack and have increased defense against that hit, plus reflecting basic projectiles back at the player.
    /// If it reaches a certain number of stacks, it'll explode in a big radius around it, dealing high aoe damage and dying in the process.
    /// On death, no matter if it exploded or not, spawns 2 Swarms that are flung a decent distance to the right and left of it.
    /// </summary>
    private enum Frame
    {
        Wander1,
        Wander2,
        Wander3,
        Notice,
        Preparing1,
        Preparing2,
        Preparing3,
        Puke1,
        Puke2,
        Puke3,
        Tired1,
        Tired2,
        Tired3,
        Fleeing1,
        Fleeing2,
        Fleeing3,
        Explode1,
        Explode2,
        Explode3
    }
    
}
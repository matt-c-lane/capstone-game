using UnityEngine;

public class PlayerStatsManager : PlayerManager
{
    // === Player Stats ===
    [SerializeField] private int _body = 1; // Don't access backing field directly, use body instead
    private int bodyMod;
    public int body { get{return _body+bodyMod;} private set{_body = value;} } //Used to make physical attacks
    [SerializeField] private int _mind = 1; // Don't access backing field directly, use mind instead
    private int mindMod;
    public int mind { get{return _mind+mindMod;} private set{_mind = value;} } //Used to make magical attacks
    [SerializeField] private int _luck = 1; // Don't access backing field directly, use luck instead
    private int luckMod;
    public int luck { get{return _luck+luckMod;} private set{_luck = value;} } //Used for crit chance

    public static bool assigned = false; //Have the stats been assigned by a PointBuyManager?

    public PlayerStatsManager(Player player)
    {
        this.player = player;
        if (!PlayerStatsManager.assigned)
        {
            this.body = PointBuyManager.bodyPoints;
            this.mind = PointBuyManager.mindPoints;
            this.luck = PointBuyManager.luckPoints;
            PlayerStatsManager.assigned = true;
        }

    }
    
    // === Stats Functions ===
    public void ModBody(int amount) { bodyMod += amount; UpdateUI(); }
    public void ModMind(int amount) { mindMod += amount; UpdateUI(); }
    public void ModLuck(int amount) { luckMod += amount; UpdateUI(); }
    private void SetAllStats(int body, int luck, int mind) { _body = body; _luck = luck; _mind = mind; UpdateUI(); }
    private void UpdateUI() { player.uiManager.UpdateStats(body, mind, luck); }
}
using System.Collections.Generic;

public class TextProvider
{
    private const string NameResetProgressPart1 = "ResetProgressPart1";
    private const string NameResetProgressPart2 = "ResetProgressPart2";

    private const string NameWarningResetProgressPart1 = "WarningResetProgressPart1";
    private const string NameWarningResetProgressPart2 = "WarningResetProgressPart2";

    private readonly Dictionary<string, Dictionary<string, string>> _texts = new()
    {
        [NameResetProgressPart1] = new()
        {
            ["ru"] = "Ты получишь",
            ["en"] = "You'll get",
            ["tr"] = "You'll get",
        },

        [NameResetProgressPart2] = new()
        {
            ["ru"] = "монет престижа",
            ["en"] = "prestige coins",
            ["tr"] = "prestige coins",
        },

        [NameWarningResetProgressPart1] = new()
        {
            ["ru"] = "Тебе нужен",
            ["en"] = "You need",
            ["tr"] = "You need",
        },

        [NameWarningResetProgressPart2] = new()
        {
            ["ru"] = "уровень или выше для сброса прогресса",
            ["en"] = "level or highter for reset progress",
            ["tr"] = "level or highter for reset progress",
        },
    };

    public static string ResetProgressPart1 { get; private set; }
    public static string ResetProgressPart2 { get; private set; }
    public static string WarningResetProgressPart1 { get; private set; }
    public static string WarningResetProgressPart2 { get; private set; }

    public void Load(string lang)
    {
        if(lang != "ru" && lang != "tr" && lang != "en")
        {
            lang = "en";
        }

        ResetProgressPart1 = _texts[NameResetProgressPart1][lang];
        ResetProgressPart2 = _texts[NameResetProgressPart2][lang];
        WarningResetProgressPart1 = _texts[NameWarningResetProgressPart1][lang];
        WarningResetProgressPart2 = _texts[NameWarningResetProgressPart2][lang];
    }
}

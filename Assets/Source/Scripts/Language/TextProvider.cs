public class TextProvider
{
    private const string RuResetProgressPart1 = "You'll get";
    private const string EnResetProgressPart1 = "Ты получишь";
    private const string TrResetProgressPart1 = "You'll get";

    private const string RuResetProgressPart2 = "монет престижа";
    private const string EnResetProgressPart2 = "prestige coins";
    private const string TrResetProgressPart2 = "prestige coins";

    private const string RuWarningResetProgressPart1 = "Тебе нужен";
    private const string EnWarningResetProgressPart1 = "You need";
    private const string TrWarningResetProgressPart1 = "You need";

    private const string RuWarningResetProgressPart2 = "уровень или выше для сброса прогресса";
    private const string EnWarningResetProgressPart2 = "level or highter for reset progress";
    private const string TrWarningResetProgressPart2 = "level or highter for reset progress";

    public static string ResetProgressPart1 { get; private set; }
    public static string ResetProgressPart2 { get; private set; }
    public static string WarningResetProgressPart1 { get; private set; }
    public static string WarningResetProgressPart2 { get; private set; }

    public void Load(string lang)
    {
        switch (lang)
        {
            case "ru":
                ResetProgressPart1 = RuResetProgressPart1;
                ResetProgressPart2 = RuResetProgressPart2;

                WarningResetProgressPart1 = RuWarningResetProgressPart1;
                WarningResetProgressPart2 = RuWarningResetProgressPart2;
                break;

            case "tr":
                ResetProgressPart1 = TrResetProgressPart1;
                ResetProgressPart2 = TrResetProgressPart2;

                WarningResetProgressPart1 = TrWarningResetProgressPart1;
                WarningResetProgressPart2 = TrWarningResetProgressPart2;
                break;

            default:
                ResetProgressPart1 = EnResetProgressPart1;
                ResetProgressPart2 = EnResetProgressPart2;

                WarningResetProgressPart1 = EnWarningResetProgressPart1;
                WarningResetProgressPart2 = EnWarningResetProgressPart2;
                break;
        }
    }
}

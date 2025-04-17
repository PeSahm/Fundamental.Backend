using System.Globalization;
using System.Text.RegularExpressions;
using DNTPersianUtils.Core;

namespace Fundamental.BuildingBlock;

public static class SymbolExtensions
{
    public static string CorrectIndicesName(this string isin, string @default = "") => isin.Trim() switch
    {
        "IRX6X23T0006" => "نفتی",
        "IRX6X44T0006" => "شیمیایی",
        "IRX6X27T0006" => "فلزات",
        "IRX6X57T0006" => "بانکی",
        "IRX6X43T0006" => "دارویی",
        "IRX6X42T0006" => "غذایی",
        "IRX6X39T0006" => "هلدینگ",
        "IRX6X56T0006" => "سرمایه گذاری",
        "IRX6X01T0006" => "زراعت",
        "IRX6X13T0006" => "کانه فلزات",
        "IRX6X34T0006" => "خودرویی",
        "IRX6X70T0006" => "انبوه سازی",
        "IRX6X60T0006" => "حمل ونقل، انبارداري و ارتباطات",
        "IRX6X35T0006" => "ساير تجهيزات حمل و نقل",
        "IRX6X53T0006" => "سیمان",
        "IRX6X31T0006" => "برقی",
        "IRX6X54T0006" => "کانی غیرفلزی",
        "IRX6X32T0006" => "وسایل ارتباطی",
        "IRX6X72T0006" => "رایانه",
        "IRX6X73T0006" => "اطلاعات و ارتباطات",
        "IRX6X67T0006" => "اداره بازارهای مالی",
        "IRX6X58T0006" => "لیزینگ",
        "IRX6X64T0006" => "رادیویی",
        "IRX6X28T0006" => "محصولات فلزی",
        "IRX6X38T0006" => "قند و شکر",
        "IRX6X29T0006" => "ماشین آلات",
        "IRX6X49T0006" => "کاشی سرامیک",
        "IRX6X25T0006" => "لاستیک",
        "IRX6X66T0006" => "بیمه",
        "IRX6X21T0006" => "محصولات کاغذ",
        "IRX6X19T0006" => "محصولات چرمی",
        "IRX6X14T0006" => "سایر معادن",
        "IRX6X11T0006" => "استخراج نفت",
        "IRX6X74T0006" => "فنی مهندسی",
        "IRX6X40T0006" => "تامین آب، برق، گاز",
        "IRX6X47T0006" => "خرده فروشی",
        "IRX6X10T0006" => "استخراج زغال سنگ",
        "IRX6X17T0006" => "منسوجات",
        "IRX6X20T0006" => "محصولات چوبی",
        "IRX6X22T0006" => "انتشار و چاپ",
        "IRX6X33T0006" => "ابزار پزشکی",
        "IRX6X45T0006" => "پیمانکاری صنعتی",

        "IRX6XTPI0006" => "شاخص كل",
        "IRX6XAFF0006" => "شاخص آزاد شناور",
        "IRX6XALS0006" => "شاخص بازار دوم",
        "IRX6XS300006" => "شاخص 30 شركت بزرگ",
        "IRX6XSLC0006" => "شاخص50شركت فعالتر",
        "IRX6XTAL0006" => "شاخص بازار اول",
        "IRX6XTDP0006" => "شاخص بازده نقدي قيمت",
        "IRX6XWAI0006" => "شاخص قيمت 50 شركت",
        "IRX6XWTH0006" => "شاخص قيمت 30 شركت",
        "IRXWXEXR0006" => "شاخص اكسير قيمت",
        "IRXWXEXR0106" => "اكسير آزاد شناورقيمت",
        "IRXYXTPI0006" => "شاخص قيمت وزني-ارزشي",
        "IRXZXCMI0006" => "شاخص اركان و نهادهاي مالي",
        "IRXZXEGS0006" => "تامين برق،گاز،بخاروآب گرم فرا",
        "IRXZXENG0006" => "شاخص فني مهندسي",
        "IRXZXEXR0006" => "شاخص اكسير كل",
        "IRXZXEXR0106" => "اكسير آزاد شناور كل",

        "IRXWXEXR0026" => "شاخص اكسير قيمت هم وزن",
        "IRX6XTPI0026" => "شاخص كل هم وزن",
        "IRXYXTPI0026" => "شاخص قيمت هم وزن",
        "IRXZXEXR0026" => "شاخص اكسير كل هم وزن",

        "IRXWXOCI0026" => "شاخص قيمت هم وزن فرابورس",
        "IRXWXOCI0106" => "آزاد شناورقيمت فرابورس",
        "IRXZXBNK0006" => "شاخص بانك فرابورس",
        "IRXZXCML0006" => "شاخص پتروشيمي فرابورس",
        "IRXZXCNS0006" => "شاخص ساخت املاك فرابورس",
        "IRXZXFIN0006" => "شاخص مالي فرابورس",
        "IRXZXFOD0006" => "شاخص خوراكي و آشاميدني فرابورس",
        "IRXZXHTL0006" => "شاخص هتل و رستوران فرابورس",
        "IRXZXICM0006" => "اطلاعات و ارتباطات فرابورس",
        "IRXZXIND0006" => "شاخص صنعت فرابورس",
        "IRXZXINS0006" => "شاخص بيمه فرابورس",
        "IRXZXINV0006" => "شاخص سرمايه گذاري فرابورس",
        "IRXZXITG0006" => "شاخص فناوري اطلاعات فرابورس",
        "IRXZXLSG0006" => "شاخص ليزينگ فرابورس",
        "IRXZXMIN0006" => "شاخص استخراج معدن فرابورس",
        "IRXZXMML0006" => "شاخص كاني فلزي فرابورس",
        "IRXZXMNF0006" => "شاخص توليد فرابورس",
        "IRXZXMOT0006" => "شاخص خودرو فرابورس",
        "IRXZXMTL0006" => "شاخص فلزات اساسي فرابورس",
        "IRXZXNMM0006" => "شاخص كاني غيرفلزي فرابورس",
        "IRXZXOBM0006" => "شاخص بازار پايه فرابورس",
        "IRXZXOCI0006" => "شاخص كل فرابورس",
        "IRXZXOCI0026" => "شاخص كل هم وزن فرابورس",
        "IRXZXOCI0106" => "آزاد شناوركل فرابورس",
        "IRXZXOEW0006" => "شاخص كل هم وزن پايه فرابورس",
        "IRXZXOIL0006" => "شاخص فرآورده هاي نفتي فرابورس",
        "IRXZXOUT0006" => "شاخص پيمانكاري فرابورس",
        "IRXZXOZ10006" => "بازار اول فرابورس",
        "IRXZXOZ20006" => "بازار دوم فرابورس",
        "IRXZXPHM0006" => "شاخص دارو فرابورس",
        "IRXZXRTL0006" => "شاخص خرده فروشي فرابورس",
        "IRXZXSRV0006" => "شاخص خدمات فرابورس",
        "IRXZXTPT0006" => "حمل و نقل و انبارداري فرابورس",
        "IRXWXOCI0006" => "شاخص قيمت فرابورس",

        "IRXZXTRI0006" => "شاخص لاستيك",
        "IRXZXWDI0006" => "شاخص چوب",
        "IRXZXAGR0006" => "شاخص كشاورزي",
        "IRXZXTEX0006" => "شاخص منسوجات",
        "IRX6XSNT0006" => "شاخص صنعت",

        _ => @default,
    };

    // Main method to extract sales information
    public static SalesInfo ExtractFezarInfo(string reportText)
    {
        SalesInfo salesInfo = new SalesInfo();

        // Extract report date
        Match dateMatch = Regex.Match(reportText, @"منتهی به (\d{4}/\d{2}/\d{2})");

        if (dateMatch.Success)
        {
            string persianDate = dateMatch.Groups[1].Value.ToEnglishNumbers();
            string[] dateParts = persianDate.Split('/');
            salesInfo.Year = int.Parse(dateParts[0]);
            salesInfo.Month = int.Parse(dateParts[1]);
            salesInfo.ReportDate = ConvertPersianToGregorianDate(persianDate).ToUniversalTime();
        }

        // Extract Persian month name
        salesInfo.PersianMonthName = ExtractPersianMonthName(reportText);

        // Extract monthly sales amount
        Match monthlySalesMatch = Regex.Match(
            reportText,
            @"در (.+?) ماه \d{4} مبلغ ([\d,،]+) میلیارد ریال است");

        if (monthlySalesMatch.Success)
        {
            salesInfo.MonthlySales = ParsePersianAmount(monthlySalesMatch.Groups[2].Value) * 1000;
        }

// Extract cumulative sales amount
        Match cumulativeSalesMatch = Regex.Match(
            reportText,
            @"تا پایان .+?(?:ماه)? \d{4} مبلغ ([\d,،]+) میلیارد ریال است");

        if (!cumulativeSalesMatch.Success)
        {
            // Try alternative pattern where month and "ماه" are combined without a space
            cumulativeSalesMatch = Regex.Match(
                reportText,
                @"تا پایان .+?ماه \d{4} مبلغ ([\d,،]+) میلیارد ریال است");
        }

        if (cumulativeSalesMatch.Success)
        {
            salesInfo.CumulativeSales = ParsePersianAmount(cumulativeSalesMatch.Groups[1].Value) * 1000;
        }

        return salesInfo;
    }

    // Process a batch of reports
    public static List<SalesInfo> ExtractFezarInfo(List<string> reports)
    {
        List<SalesInfo> results = new List<SalesInfo>();

        foreach (string report in reports)
        {
            SalesInfo salesInfo = ExtractFezarInfo(report);
            results.Add(salesInfo);
        }

        return results;
    }

    // Helper method to parse Persian numbers and commas
    private static decimal ParsePersianAmount(string amountStr)
    {
        amountStr = amountStr.ToEnglishNumbers();

        // Replace Persian commas with standard commas
        amountStr = amountStr.Replace('،', ',');

        // Remove all commas
        amountStr = amountStr.Replace(",", string.Empty);

        return decimal.Parse(amountStr, CultureInfo.InvariantCulture);
    }

    // Helper method to extract Persian month name
    private static string ExtractPersianMonthName(string text)
    {
        Match monthMatch = Regex.Match(text, @"(?:در|پایان) (شهریور|مهر|آبان|آذر|دی|بهمن|اسفند|فروردین|اردیبهشت|خرداد|تیر|مرداد) ماه");
        return monthMatch.Success ? monthMatch.Groups[1].Value : string.Empty;
    }

    // Helper method to convert Persian date to Gregorian
    private static DateTime ConvertPersianToGregorianDate(string persianDate)
    {
        return persianDate.ToGregorianDateTime() ?? DateTime.Now;
    }

    // Models to store extracted data
    public class SalesInfo
    {
        public DateTime ReportDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal MonthlySales { get; set; }
        public decimal CumulativeSales { get; set; }
        public string PersianMonthName { get; set; }
    }
}
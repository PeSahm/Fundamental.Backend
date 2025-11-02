namespace Fundamental.IntegrationTests.TestData;

public static class MonthlyActivityTestData
{
    public static string GetV5TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "IntegrationTests",
            "Data",
            "MonthlyActivites",
            "V5",
            "Sorood-.1404-07-30.json");

        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }

        // Fallback to minimal test data if file not found
        return """
               {
                   "monthlyActivity": {
                       "version": "5",
                       "productionAndSales": {
                           "yearData": [
                               {
                                   "columnId": "119714",
                                   "caption": "تعداد تولید - دوره یک ماهه",
                                   "periodEndToDate": "1404/07/30",
                                   "yearEndToDate": "1404/12/29",
                                   "period": 7,
                                   "isAudited": false
                               }
                           ],
                           "rowItems": [
                               {
                                   "rowCode": 1,
                                   "category": 1,
                                   "oldFieldName": "",
                                   "productId": 1,
                                   "unitId": 1,
                                   "rowType": "ProductRow",
                                   "value_11971": "محصول ۱",
                                   "value_11972": "کیلوگرم",
                                   "value_119714": "1000",
                                   "value_119715": "950",
                                   "value_119716": "50000",
                                   "value_119717": "47500"
                               }
                           ]
                       },
                       "energy": {
                           "yearData": [
                               {
                                   "columnId": "319514",
                                   "caption": "مقدار مصرف - دوره یک ماهه",
                                   "periodEndToDate": "1404/07/30",
                                   "yearEndToDate": "1404/12/29",
                                   "period": 7,
                                   "isAudited": false
                               }
                           ],
                           "rowItems": [
                               {
                                   "rowCode": 1,
                                   "oldFieldName": "",
                                   "category": 1,
                                   "rowType": "FixedRow",
                                   "value_31951": "برق",
                                   "value_31952": "صنعتی",
                                   "value_31953": "کیلووات ساعت",
                                   "value_31954": "متر مکعب",
                                   "value_319514": "5000"
                               }
                           ]
                       },
                       "productMonthlyActivityDesc1": {
                           "rowItems": [
                               {
                                   "category": 1,
                                   "rowCode": 1,
                                   "rowType": "DescriptionRow",
                                   "oldFieldName": "",
                                   "value_11991": "توضیحات نمونه برای فعالیت ماهانه"
                               }
                           ]
                       },
                       "buyRawMaterial": {
                           "yearData": [
                               {
                                   "columnId": "346412",
                                   "caption": "خرید دوره یک ماهه - مقدار",
                                   "periodEndToDate": "1404/07/30",
                                   "yearEndToDate": "1404/12/29",
                                   "period": 7,
                                   "isAudited": false
                               }
                           ],
                           "rowItems": [
                               {
                                   "rowCode": 1,
                                   "oldFieldName": "",
                                   "category": 1,
                                   "rowType": "FixedRow",
                                   "value_34641": "مواد اولیه ۱",
                                   "value_34642": "کیلوگرم",
                                   "value_346412": "2000"
                               }
                           ]
                       },
                       "sourceUsesCurrency": {
                           "yearData": [
                               {
                                   "columnId": "364012",
                                   "caption": "دوره یکماهه - مبلغ ارزی",
                                   "periodEndToDate": "1404/07/30",
                                   "yearEndToDate": "1404/12/29",
                                   "period": 7,
                                   "isAudited": false
                               }
                           ],
                           "rowItems": [
                               {
                                   "rowCode": 1,
                                   "oldFieldName": "",
                                   "category": 1,
                                   "rowType": "FixedRow",
                                   "value_36401": "دلار آمریکا",
                                   "value_36402": "USD",
                                   "value_364012": "10000"
                               }
                           ]
                       }
                   },
                   "listedCapital": "10000000",
                   "unauthorizedCapital": "0"
               }
               """;
    }

    public static string GetV4TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "IntegrationTests",
            "Data",
            "MonthlyActivites",
            "V4",
            "Sorood-1402-01-31.json");

        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }

        // Fallback V4 data (similar to V5 but without energy and currency sections)
        return """
               {
                   "monthlyActivity": {
                       "version": "4",
                       "productionAndSales": {
                           "yearData": [
                               {
                                   "columnId": "119714",
                                   "caption": "تعداد تولید - دوره یک ماهه",
                                   "periodEndToDate": "1402/01/31",
                                   "yearEndToDate": "1402/12/29",
                                   "period": 1,
                                   "isAudited": false
                               }
                           ],
                           "rowItems": [
                               {
                                   "rowCode": 1,
                                   "category": 1,
                                   "oldFieldName": "",
                                   "productId": 1,
                                   "unitId": 1,
                                   "rowType": "ProductRow",
                                   "value_11971": "محصول ۱",
                                   "value_11972": "کیلوگرم",
                                   "value_119714": "1000",
                                   "value_119715": "950",
                                   "value_119716": "50000",
                                   "value_119717": "47500"
                               }
                           ]
                       },
                       "productMonthlyActivityDesc1": {
                           "rowItems": [
                               {
                                   "category": 1,
                                   "rowCode": 1,
                                   "rowType": "DescriptionRow",
                                   "oldFieldName": "",
                                   "value_11991": "توضیحات نمونه برای فعالیت ماهانه V4"
                               }
                           ]
                       },
                       "buyRawMaterial": {
                           "yearData": [
                               {
                                   "columnId": "346412",
                                   "caption": "خرید دوره یک ماهه - مقدار",
                                   "periodEndToDate": "1402/01/31",
                                   "yearEndToDate": "1402/12/29",
                                   "period": 1,
                                   "isAudited": false
                               }
                           ],
                           "rowItems": [
                               {
                                   "rowCode": 1,
                                   "oldFieldName": "",
                                   "category": 1,
                                   "rowType": "FixedRow",
                                   "value_34641": "مواد اولیه ۱",
                                   "value_34642": "کیلوگرم",
                                   "value_346412": "2000"
                               }
                           ]
                       }
                   },
                   "listedCapital": "10000000",
                   "unauthorizedCapital": "0"
               }
               """;
    }

    public static string GetV3TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "IntegrationTests",
            "Data",
            "MonthlyActivites",
            "V3");

        string[] files = Directory.GetFiles(filePath, "*.json");

        if (files.Length > 0)
        {
            return File.ReadAllText(files[0]);
        }

        // Fallback V3 data (production and sales + descriptions only)
        return """
               {
                   "monthlyActivity": {
                       "version": "3",
                       "productionAndSales": {
                           "yearData": [
                               {
                                   "columnId": "119714",
                                   "caption": "تعداد تولید - دوره یک ماهه",
                                   "periodEndToDate": "1401/01/31",
                                   "yearEndToDate": "1401/12/29",
                                   "period": 1,
                                   "isAudited": false
                               }
                           ],
                           "rowItems": [
                               {
                                   "rowCode": 1,
                                   "category": 1,
                                   "oldFieldName": "",
                                   "productId": 1,
                                   "unitId": 1,
                                   "rowType": "ProductRow",
                                   "value_11971": "محصول ۱",
                                   "value_11972": "کیلوگرم",
                                   "value_119714": "1000",
                                   "value_119715": "950",
                                   "value_119716": "50000",
                                   "value_119717": "47500"
                               }
                           ]
                       },
                       "productMonthlyActivityDesc1": {
                           "rowItems": [
                               {
                                   "category": 1,
                                   "rowCode": 1,
                                   "rowType": "DescriptionRow",
                                   "oldFieldName": "",
                                   "value_11991": "توضیحات نمونه برای فعالیت ماهانه V3"
                               }
                           ]
                       }
                   },
                   "listedCapital": "10000000",
                   "unauthorizedCapital": "0"
               }
               """;
    }

    public static string GetV2TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "IntegrationTests",
            "Data",
            "MonthlyActivites",
            "V2");

        string[] files = Directory.GetFiles(filePath, "*.json");

        if (files.Length > 0)
        {
            return File.ReadAllText(files[0]);
        }

        // Fallback V2 data (uses productAndSales as array)
        return """
               {
                   "monthlyActivity": {
                       "version": "2",
                       "productAndSales": [
                           {
                               "financialYear": {
                                   "priodEndToDate": "1399/01/31",
                                   "prevPeriodEndToDate": "1398/12/29"
                               },
                               "fields": {
                                   "productName": "محصول ۱",
                                   "unitName": "کیلوگرم",
                                   "production": 1000,
                                   "sales": 950,
                                   "salesPrice": 50000,
                                   "salesAmount": 47500
                               }
                           }
                       ],
                       "productMonthlyActivityDesc1": {
                           "rowItems": [
                               {
                                   "category": 1,
                                   "rowCode": 1,
                                   "rowType": "DescriptionRow",
                                   "oldFieldName": "",
                                   "value_11991": "توضیحات نمونه برای فعالیت ماهانه V2"
                               }
                           ]
                       }
                   },
                   "listedCapital": "10000000",
                   "unauthorizedCapital": "0"
               }
               """;
    }

    public static string GetV1TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "IntegrationTests",
            "Data",
            "MonthlyActivites",
            "V1");

        string[] files = Directory.GetFiles(filePath, "*.json");

        if (files.Length > 0)
        {
            return File.ReadAllText(files[0]);
        }

        // Fallback V1 data (minimal structure)
        return """
               {
                   "monthlyActivity": {
                       "version": "1",
                       "productAndSales": [
                           {
                               "description": "محصول ۱",
                               "unit": "کیلوگرم",
                               "production": 1000,
                               "sales": 950,
                               "price": 50000,
                               "amount": 47500
                           }
                       ]
                   },
                   "listedCapital": "10000000",
                   "unauthorizedCapital": "0"
               }
               """;
    }
}
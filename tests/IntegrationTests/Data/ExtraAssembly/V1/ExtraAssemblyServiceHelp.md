### **پیوست : تصمیمات مجمع عمومی فوق العاده**
**نسخه شماره : 1**
**از تاریخ : 1388/11/07**

---

### **decision-ExtraAssembly (تصمیمات مجمع فوق‌العاده)**

| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| capitalChangeState | int | **وضعیت تغییر سرمایه:** <br> `1`: تصمیم‌گیری در خصوص افزایش سرمایه <br> `2`: تصمیم‌گیری در خصوص کاهش سرمایه <br> `3`: تغییر ارزش اسمی <br> `0`: None |
| lastshareValue | string | آخرین سرمایه ثبت شده - ارزش اسمی هر سهم(ریال) |
| lastCapital | string | آخرین سرمایه ثبت شده - مبلغ (میلیون ریال) |
| lastshareCount | string | آخرین سرمایه ثبت شده - تعداد سهام |
| oldAddress | string | آدرس قبلی |
| newAddress | string | آدرس جدید |
| oldName | string | نام قبلی |
| newName | string | نام جدید |
| oldActivitySubject | string | عنوان فعالیت قبلی |
| newActivitySubject | string | عنوان فعالیت جدید |
| oldFinancialYearMonthLenght | string | سال مالی قبلی - دوره (ماه) |
| oldFinancialYearEndDate | string | سال مالی قبلی |
| oldFinancialYearDayLenght | string | سال مالی قبلی - دوره (روز) |
| newFinancialYearEndDate | string | سال مالی جدید |
| newFinancialYearMonthLenght | string | سال مالی جدید - دوره (ماه) |
| newFinancialYearDayLenght | string | سال مالی جدید - دوره (روز) |
| isLocationChange | Boolean | تغییر محل |
| isNameChange | Boolean | تغییر نام |
| isActivitySubjectChange | Boolean | تغییر نوع فعالیت |
| isFinancialYearChange | Boolean | تغییر سال مالی منتهی به |
| isDecidedClause141 | Boolean | تصمیم گیری در خصوص شمولیت مفاد ماده 141 قانون تجارت |
| decidedClause141Des | string | تصمیم گیری در خصوص شمولیت مفاد ماده 141 قانون تجارت - توضیحات |
| isAccordWithSEOStatuteApproved | Boolean | تطابق اساسنامه شرکت با نمونه اساسنامه سازمان بورس |
| otherDes | string | سایر موارد - توضیحات |
| primaryMarketTracingNo | int | شماره پیگیری مجوز افزایش سرمایه |
| correctionStatuteApproved | Boolean | **اصلاح اساسنامه (انتخاب سامانه کدال):** <br> `1`: تصویب شد <br> `0`: تصویب نشد |
| independentAuditorLegalInspectorRepresentative | string | نماینده بازرس و حسابرس قانونی |
| topFinancialPosition | string | بالاترین مقام مالی |

---

#### **parentAssembly (مجمع اصلی)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| assemblyResultType | int | **نتیجه مجمع:** <br> `1`: در خصوص دستور جلسات زیر تصمیم گیری نمود <br> `2`: به حد نصاب قانونی نرسید <br> `3`: در خصوص برخی دستور جلسات تصمیم گیری ننمود و تصمیم گیری به مجمع دیگری موکول گردید <br> `4`: مجمع در خصوص موارد زیر تصمیم گیری نمود و در مورد بقیه موارد با تنفس روبرو گردید <br> `5`: مجمع در خصوص هیچ یک از موارد دستور جلسه تصمیم گیری ننمود و مجمع با تنفس روبرو گردید |
| assemblyResultTypeTitle | string | عنوان نتیجه مجمع |
| date | string | تاریخ برگزاری مجمع |
| hour | string | ساعت برگزاری مجمع |
| location | string | محل برگزاری مجمع |
| day | string | روز برگزاری مجمع |
| letterPublishDate | string | تاریخ انتشار اطلاعیه |
| letterTracingNo | int | شماره پیگیری اطلاعیه قبلی |

---

#### **sessionOrders (دستور جلسات)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| type | int | نوع دستور جلسه |
| description | string | عنوان فارسی دستور جلسه |
| fieldName | string | عنوان انگلیسی دستور جلسه |

---

#### **shareHolders (اطلاعات ترکیب سهامداران)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| shareHolderSerial | int | سریال سهامدار |
| name | string | نام سهامدار |
| shareCount | long | تعداد سهام |
| sharePercent | float | درصد مالکیت |

---

#### **assemblyChiefMembers (اعضای هیئت رئیسه مجمع)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| assemblyChief | string | رئیس مجمع |
| assemblySuperVisor1 | string | ناظرمجمع |
| assemblySuperVisor2 | string | ناظرمجمع |
| assemblySecretary | string | منشی مجمع |

---

#### **extraAssemblyIncreaseCapitals (افزایش سرمایه)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| cashIncoming | int | مطالبات و اوردۀ نقدی (میلیون ریال) |
| retaindedEarning | int | سود انباشته (میلیون ریال) |
| reserves | int | اندوخته (میلیون ریال) |
| revaluationSurplus | int | مازاد تجدید ارزیابی دارایی ها (میلیون ریال) |
| sarfSaham | int | صرف سهام (میلیون ریال) |
| isAccept | Boolean | موافقت/عدم‌موافقت |
| capitalIncreaseValue | int | مبلغ افزایش سرمایه (میلیون ریال) |
| increasePercent | decimal | درصد افزایش سرمایه |
| type | short | **نحوۀ تصویب:** <br> `0`: قطعی <br> `1`: در اختیار هیئت‌مدیره |
| cashForceclosurePriorityStockPrice | decimal | قیمت سهام جهت عرضه عمومی-ریال |
| cashForceclosurePriorityStockDesc | string | توضیحات در خصوص قیمت سهام جهت عرضه عمومی |
| cashForceclosurePriorityAvailableStockCount | int | تعداد سهام قابل عرضه به عموم |
| cashForceclosurePriorityPrizeStockCount | int | تعداد سهام جایزه |
| cashForceclosurePriority | decimal | آورده نقدی با سلب حق تقدم از سهامداران فعلی |

---

#### **extraAssemblyDecreaseCapital (کاهش سرمایه)**

| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| capitalDecreaseValue | decimal | مبلغ کاهش سرمایه |
| decreasePercent | decimal | درصد کاهش سرمایه |
| isAccept | bool | موافقت/عدم‌موافقت |
| newCapital | long | مبلغ سرمایه جدید (میلیون ریال) |
| newShareCount | long | تعداد سهام جدید |
| newShareValue | int | ارزش اسمی هر سهم جدید |

---

#### **extraAssemblyShareValueChangeCapitals (تغییر ارزش اسمی)**

| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| isAccept | bool | موافقت/عدم‌موافقت |
| newShareCount | long | تعداد سهام جدید |
| newShareValue | int | ارزش اسمی هر سهم جدید |

---

#### **extraAssemblyScheduling (زمانبندی)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| isRegistered | Boolean | به دارندگان حق تقدم سودی تعلق ؟ |
| yearEndToDate | string | با توجه به زمان برگزاری مجمع عمومی عادی سالیانه منتهی به |

---

#### **nextSession (مجمع بعدی - اعلام تنفس)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| breakDesc | string | توضیحات اعلام تنفس |
| hour | string | ساعت برگزاری مجمع بعدی |
| date | string | تاریخ برگزاری مجمع بعدی |
| day | string | روز برگزاری مجمع بعدی |
| location | string | محل برگزاری مجمع بعدی |

---

#### **assemblyBoardMembers (اعضای هیئت مدیره حاضر)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| boardMemberSerial | int | سریال عضو حقیقی یا حقوقی هیئت مدیره |
| fullName | string | نام عضو حقیقی یا حقوقی هیئت مدیره |
| nationalCode | string | شمارۀ ثبت عضو حقوقی/کد ملی |
| legalType | short | **نوع شرکت:** <br> `0`: Nothing, `1`: General, `2`: Special, `3`: Limited, `4`: Guaranty, `5`: MotleyNoneShare, `6`: MotleyShare, `7`: Comparative, `8`: Communion, `9`: NoneCommerce |
| membershipType | short | **نوع عضویت:** <br> `0`: علی‌البدل, `1`: اصلی |
| agentBoardMemberFullName | string | نام نماینده عضو حقوقی |
| agentBoardMemberNationalCode | string | کد ملی نماینده عضو حقوقی |
| position | string | **سمت:** <br> `0`: عضو هیئت مدیره, `1`: نایب رئیس هیئت مدیره, `2`: رئیس هیئت مدیره |
| hasDuty | Boolean | موظف/غیر موظف |
| degree | string | مقطع تحصیلی |
| attendingMeeting | int | **آیا در جلسه حضور داشته؟** <br> `0`: عدم حضور در جلسه, `1`: حضور در جلسه |
| educationField | string | رشته ی تحصیلی |
| verification | int | **تایید صلاحیت:** <br> `1`: تایید صلاحیت شده, `2`: در حال تایید صلاحیت |
| degreeRef | int | سریال مقطع تحصیلی |
| educationFieldRef | int | سریال رشته ی تحصیلی |

---
#### **auditCommitteeChairman (رئیس کمیته حسابرسی)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| fullName | string | نام رئیس کمیته حسابرسی |
| nationalCode | string | کد ملی |
| attendingMeeting | int | آیا در جلسه حضور داشته؟ |
| degreeRef | int | سریال مقطع تحصیلی |
| educationFieldRef | int | سریال رشته ی تحصیلی |
| degree | string | مقطع تحصیلی |
| educationField | string | رشته ی تحصیلی |
| verification | string | تایید صلاحیت |

---
#### **ceo (مدیر عامل)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| attendingMeeting | int | آیا در جلسه حضور داشته؟ |
| fullName | string | نام مدیر عامل |
| nationalCode | string | کد ملی |
| degreeRef | int | سریال مقطع تحصیلی |
| educationFieldRef | int | سریال رشته ی تحصیلی |
| degree | string | مقطع تحصیلی |
| educationField | string | رشته ی تحصیلی |
| verification | string | تایید صلاحیت |
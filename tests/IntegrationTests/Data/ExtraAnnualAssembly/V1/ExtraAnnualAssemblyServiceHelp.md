### **پیوست : تصمیمات مجمع عمومی عادی به طور فوق العاده**
**نسخه شماره : 1**
**از تاریخ : 1388/22/10**

---

### **decision-ExtraAnnualAssemblyStatement (تصمیمات مجمع)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| boardMemberPeriod | int | مدت انتخاب اعضای هیئت مدیره |
| publishSecurityDes | string | توضیحات انتشار اوراق بهادار غیر قابل تبدیل یا تعویض با سهام |
| otherDescription | string | سایر توضیحات |
| newHour | string | ساعت برگزاری مجمع بعدی - درصورت اعلام تنفس |
| newDay | string | روز برگزاری مجمع بعدی - درصورت اعلام تنفس |
| newDate | string | تاریخ برگزاری مجمع بعدی - درصورت اعلام تنفس |
| newLocation | string | محل برگزاری مجمع بعدی - درصورت اعلام تنفس |
| breakDes | string | توضیحات اعلام تنفس |
| independentAuditorLegalInspectorRepresentative | string | نماینده بازرس و حسابرس قانونی |
| topFinancialPosition | string | بالاترین مقام مالی |

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
| description | string | عنوان فارسی دستورجلسه |
| fieldName | string | عنوان انگلیسی دستورجلسه |

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
#### **assemblyBoardMembers (اعضای هیئت مدیره حاضر در مجمع)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| boardMemberSerial | int | سریال عضو حقیقی یا حقوقی هیئت مدیره |
| fullName | string | نام عضو حقیقی یا حقوقی هیئت مدیره |
| nationalCode | string | شمارۀ ثبت عضو حقوقی/کد ملی |
| legalType | short | **نوع شرکت:** <br> `0`: Nothing, `1`: General, `2`: Special, `3`: Limited, `4`: Guaranty, `5`: MotleyNoneShare, `6`: MotleyShare, `7`: Comparative, `8`: Communion, `9`: NoneCommerce |
| membershipType | short | **نوع عضویت:** <br> `0`: علی البدل, `1`: اصلی |
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
#### **assemblyInterim (اطلاعات میان دوره‌ای)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| fieldName | string | نام فیلد |
| description | string | توضیحات |
| yearEndToDateValue | int | مقدار سال منتهی به |
| percent | float | درصد تغییرات |
| changesReason | string | دلایل تغییرات |
| rowClass | string | کلاس ردیف |

---
#### **assemblyProportionedRetainedEarning (سود انباشته)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| fieldName | string | نام فیلد |
| description | string | توضیحات |
| yearEndToDateValue | int | مقدار سال منتهی به |
| rowClass | string | کلاس ردیف |

---
#### **inspectors (حسابرسان و بازرسان)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| serial | int | سریال حسابرس |
| name | string | نام حسابرس |
| type | short | **نوع:** <br> `0`: قانونی, `1`: علی البدل |

---
#### **newBoardMembers (اعضای هیئت مدیره جدید)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| name | string | نام عضو حقیقی یا حقوقی هیئت مدیره |
| isLegal | Boolean | **ماهیت:** <br> `1`: حقوقی, `0`: حقیقی |
| nationalCode | string | شمارۀ ثبت عضو حقوقی/کد ملی |
| boardMemberSerial | int | شناسه عضو حقیقی یا حقوقی هیئت مدیره |
| legalType | string | **نوع شرکت:** <br> `0`: Nothing, `1`: General, `2`: Special, `3`: Limited, `4`: Guaranty, `5`: MotleyNoneShare, `6`: MotleyShare, `7`: Comparative, `8`: Communion, `9`: NoneCommerce |
| membershipType | short | **نوع عضویت:** <br> `0`: علی البدل, `1`: اصلی |

---
#### **boardMemberWageAndGift (حق حضور و پاداش هیئت مدیره)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| type | string | **نوع فیلد:** <br> `0`: حق حضور <br> `1`: پاداش <br> `2`: حق حضور اعضای هیات مدیره عضو کمیته حسابرسی <br> `3`: حق حضور اعضای هیات مدیره عضو کمیته انتصابات <br> `4`: سایر کمیته های تخصصی <br> `5`: هزینه های مسولیت اجتماعی |
| fieldName | string | عنوان فیلد |
| currentYearValue | int | سال جاری - مبلغ |
| pastYearValue | int | سال قبل - مبلغ |
| description | string | توضیحات |

---
#### **newsPapers (روزنامه‌ها)**
| Field Name | Data Type | Description (Farsi) |
| :--- | :--- | :--- |
| newsPaperId | int | شناسه روزنامه |
| name | string | نام روزنامه |

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
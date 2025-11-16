# Angular UI Implementation Guide - Annual General Meeting Decisions

## Overview

This guide provides detailed instructions for implementing the Angular UI for displaying Annual General Meeting Decisions (Annual Assembly) data from Iranian financial statements (CODAL). The implementation includes:

- **List View**: Filterable, paginated table of annual assembly reports
- **Detail View**: Comprehensive display with 9+ data grids showing assembly decisions, board members, shareholders, financial distributions
- **Persian/RTL Support**: Right-to-left layout with Persian numbers and dates
- **Enum Handling**: Type-safe handling of assembly result types, board positions, inspector types, etc.

## API Endpoints

### Base URL
```
/api/v1/Manufacturing/annual-assembly
```

### 1. Get List (Paginated)
```typescript
GET /api/v1/Manufacturing/annual-assembly
Query Parameters:
  - Isin?: string (e.g., "IRO1BAHN0001")
  - FiscalYear?: number (e.g., 1404)
  - ReportMonth?: number (1-12)
  - PageNumber: number (default: 1)
  - PageSize: number (default: 10, max: 100)

Response: Paginated<AnnualAssemblyListItem>
```

### 2. Get Detail by ID
```typescript
GET /api/v1/Manufacturing/annual-assembly/{id}
Path Parameters:
  - id: string (GUID)

Response: AnnualAssemblyDetail
Error Codes:
  - 13_374_101: "تصمیمات مجمع عمومی عادی سالیانه یافت نشد." (Not Found)
```

## TypeScript Data Structures

### List Item Interface
```typescript
export interface AnnualAssemblyListItem {
  id: string; // GUID
  isin: string; // Symbol ISIN code (e.g., "IRO1BAHN0001")
  symbol: string; // Symbol code (e.g., "فباهنر")
  title: string; // Company title
  htmlUrl: string; // CODAL report URL
  version: string; // Version (e.g., "V1")
  fiscalYear: number; // e.g., 1404
  yearEndMonth: number; // 1-12
  assemblyDate: string; // ISO 8601 date-time
  traceNo: number; // CODAL trace number
  publishDate: string; // ISO 8601 date-time
  assemblyResultTypeTitle: string; // Persian title of assembly result
}

export interface Paginated<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
```

### Detail Item Interface
```typescript
export interface AnnualAssemblyDetail {
  // Header Information
  id: string;
  isin: string;
  symbol: string;
  title: string;
  htmlUrl: string;
  version: string;
  fiscalYear: number;
  yearEndMonth: number;
  reportMonth: number;
  assemblyDate: string;
  traceNo: number;
  publishDate: string;

  // Assembly Information
  assemblyResultType: AssemblyResultType;
  assemblyResultTypeTitle: string | null;
  assemblyHour: string | null;
  assemblyLocation: string | null;
  assemblyDay: string | null;
  letterTracingNo: number | null;
  assemblyChief: string | null;
  assemblySuperVisor1: string | null;
  assemblySuperVisor2: string | null;
  assemblySecretary: string | null;

  // Additional Information
  boardMemberPeriod: string | null;
  publishSecurityDescription: string | null;
  otherDescription: string | null;
  newHour: string | null;
  newDay: string | null;
  newDate: string | null;
  newLocation: string | null;
  breakDescription: string | null;

  // Collections (9 grids)
  sessionOrders: SessionOrder[];
  shareHolders: ShareHolder[];
  assemblyBoardMembers: AssemblyBoardMember[];
  inspectors: Inspector[];
  newBoardMembers: NewBoardMember[];
  boardMemberWageAndGifts: BoardMemberWageAndGift[];
  newsPapers: NewsPaper[];
  assemblyInterims: AssemblyInterim[];
  proportionedRetainedEarnings: ProportionedRetainedEarning[];

  // Individual Attendees
  ceo: AssemblyAttendee | null;
  auditCommitteeChairman: AssemblyAttendee | null;
  independentAuditorRepresentative: AssemblyAttendee | null;
  topFinancialPosition: AssemblyAttendee | null;
}
```

### Grid Data Interfaces

#### 1. Session Orders (دستور جلسات)
```typescript
export interface SessionOrder {
  type: SessionOrderType;
  description: string | null;
  fieldName: string | null;
}

export enum SessionOrderType {
  BalanceSheetApproval = 0,                    // تصویب ترازنامه
  IncomeStatementApproval = 1,                 // تصویب صورت سود و زیان
  CashFlowStatementApproval = 2,               // تصویب صورت جریان وجوه نقد
  BoardReportApproval = 3,                     // تصویب گزارش هیئت مدیره
  AuditorReportApproval = 4,                   // تصویب گزارش بازرس
  BoardMemberElection = 5,                     // انتخاب اعضای هیئت مدیره
  InspectorElection = 6,                       // انتخاب بازرس
  OfficialNewspaperSelection = 7,              // انتخاب روزنامه کثیرالانتشار
  ProfitDistribution = 8,                      // تصویب نحوه تقسیم سود
  CapitalIncrease = 9,                         // افزایش سرمایه
  Other = 10                                   // سایر
}
```

#### 2. Shareholders (سهامداران حاضر در مجمع)
```typescript
export interface ShareHolder {
  shareHolderSerial: number | null;            // ردیف
  name: string | null;                          // نام سهامدار
  shareCount: number | null;                    // تعداد سهام
  sharePercent: number | null;                  // درصد سهام
}
```

#### 3. Assembly Board Members (هیئت رئیسه مجمع)
```typescript
export interface AssemblyBoardMember {
  boardMemberSerial: number | null;            // ردیف
  fullName: string | null;                     // نام و نام خانوادگی
  nationalCode: string | null;                 // کد ملی / شناسه ملی
  legalType: LegalCompanyType | null;          // نوع شخصیت حقوقی
  membershipType: BoardMembershipType;         // نوع عضویت
  agentBoardMemberFullName: string | null;     // نماینده
  agentBoardMemberNationalCode: string | null; // کد ملی نماینده
  position: BoardPosition;                     // سمت
  hasDuty: boolean;                            // دارای وظیفه
  degree: string | null;                       // مدرک تحصیلی
  degreeRef: number | null;                    // کد مدرک تحصیلی
  educationField: string | null;               // رشته تحصیلی
  educationFieldRef: number | null;            // کد رشته تحصیلی
  attendingMeeting: boolean;                   // حضور در جلسه
  verification: VerificationStatus;            // وضعیت احراز
}

export enum LegalCompanyType {
  RealPerson = 0,                              // شخص حقیقی
  PrivateCompany = 1,                          // شرکت خصوصی
  PublicCompany = 2,                           // شرکت سهامی عام
  CooperativeCompany = 3,                      // شرکت تعاونی
  Other = 4                                    // سایر
}

export enum BoardMembershipType {
  FullTime = 0,                                // تمام وقت
  PartTime = 1,                                // پاره وقت
  NonExecutive = 2                             // غیرموظف
}

export enum BoardPosition {
  Chairman = 0,                                // رئیس هیئت مدیره
  ViceChairman = 1,                            // نایب رئیس
  CEO = 2,                                     // مدیرعامل
  Member = 3,                                  // عضو
  Observer = 4                                 // ناظر
}

export enum VerificationStatus {
  Verified = 0,                                // احراز شده
  NotVerified = 1,                             // احراز نشده
  InProgress = 2                               // در حال بررسی
}
```

#### 4. Inspectors (بازرسان)
```typescript
export interface Inspector {
  serial: number | null;                       // ردیف
  name: string | null;                         // نام و نام خانوادگی
  type: InspectorType;                         // نوع
}

export enum InspectorType {
  Primary = 0,                                 // اصلی
  Alternate = 1,                               // علی‌البدل
  LegalInspector = 2                          // بازرس قانونی
}
```

#### 5. New Board Members (اعضای جدید هیئت مدیره)
```typescript
export interface NewBoardMember {
  name: string | null;                         // نام و نام خانوادگی
  isLegal: boolean;                            // شخص حقوقی
  nationalCode: string | null;                 // کد ملی / شناسه ملی
  boardMemberSerial: number | null;            // ردیف
  legalType: LegalCompanyType | null;          // نوع شخصیت حقوقی
  membershipType: BoardMembershipType;         // نوع عضویت
}
```

#### 6. Board Member Wages and Gifts (حق الزحمه و هدایای اعضای هیئت مدیره)
```typescript
export interface BoardMemberWageAndGift {
  type: WageAndGiftFieldType;                  // نوع
  fieldName: string | null;                    // عنوان
  currentYearValue: number | null;             // سال جاری (ریال)
  pastYearValue: number | null;                // سال قبل (ریال)
  description: string | null;                  // توضیحات
}

export enum WageAndGiftFieldType {
  ChairmanWage = 0,                            // حق الزحمه رئیس هیئت مدیره
  MembersWage = 1,                             // حق الزحمه اعضای هیئت مدیره
  CEOWage = 2,                                 // حق الزحمه مدیرعامل
  Gifts = 3,                                   // هدایا
  BonusShares = 4,                             // سهام جایزه
  TotalCompensation = 5,                       // جمع
  Other = 6                                    // سایر
}
```

#### 7. Newspapers (روزنامه‌های کثیرالانتشار)
```typescript
export interface NewsPaper {
  newsPaperId: number | null;                  // شناسه روزنامه
  name: string | null;                         // نام روزنامه
}
```

#### 8. Assembly Interims (اقلام میان دوره‌ای)
```typescript
export interface AssemblyInterim {
  fieldName: string | null;                    // عنوان
  description: string | null;                  // توضیحات
  yearEndToDateValue: number | null;           // مبلغ از ابتدای سال تا پایان دوره (ریال)
  percent: number | null;                      // درصد
  changesReason: string | null;                // علت تغییرات
  rowClass: string | null;                     // کلاس ردیف
}
```

#### 9. Proportioned Retained Earnings (تقسیم سود انباشته)
```typescript
export interface ProportionedRetainedEarning {
  fieldName: ProportionedRetainedEarningFieldName | null;
  description: string | null;                  // شرح
  yearEndToDateValue: number | null;           // مبلغ (ریال)
  rowClass: string | null;                     // کلاس ردیف
}

export enum ProportionedRetainedEarningFieldName {
  NetIncomeLoss = 0,                           // سود (زیان) خالص
  BeginingRetainedEarnings = 1,                // سود (زیان) انباشته ابتدای دوره
  AnnualAdjustment = 2,                        // تعدیلات سنواتی
  AdjustedBeginingRetainedEarnings = 3,        // سود (زیان) انباشته ابتدای دوره تعدیل‌شده
  PreYearDevidedRetainedEarning = 4,           // سود سهام مصوب (مجمع سال قبل)
  TransferToCapital = 5,                       // تغییرات سرمایه از محل سود (زیان) انباشته
  UnallocatedRetainedEarningsAtTheBeginningOfPeriod = 6, // سود انباشته ابتدای دوره تخصیص نیافته
  TransfersFromOtherEquityItems = 7,           // انتقال از سایر اقلام حقوق صاحبان سهام
  ProportionableRetainedEarnings = 8,          // سود قابل تخصیص
  LegalReserve = 9,                            // انتقال به اندوخته‌ قانونی
  ExtenseReserve = 10,                         // انتقال به سایر اندوخته‌ها
  EndingRetainedEarnings = 11,                 // سود (زیان) انباشته پايان دوره
  DividedRetainedEarning = 12,                 // سود سهام مصوب (مجمع سال جاری)
  TotalEndingRetainedEarnings = 13,            // سود (زیان) انباشته پایان دوره (با لحاظ نمودن مصوبات مجمع)
  EarningsPerShareAfterTax = 14,               // سود (زیان) خالص هر سهم- ریال
  DividendPerShare = 15,                       // سود نقدی هر سهم (ریال)
  ListedCapital = 16                           // سرمایه
}
```

#### 10. Assembly Attendee (حضور افراد خاص)
```typescript
export interface AssemblyAttendee {
  fullName: string | null;                     // نام و نام خانوادگی
  nationalCode: string | null;                 // کد ملی
  attendingMeeting: boolean;                   // حضور در جلسه
  degree: string | null;                       // مدرک تحصیلی
  degreeRef: number | null;                    // کد مدرک تحصیلی
  educationField: string | null;               // رشته تحصیلی
  educationFieldRef: number | null;            // کد رشته تحصیلی
  verification: VerificationStatus | null;     // وضعیت احراز
}
```

## Component Architecture

### Recommended Structure
```
src/app/codal/
├── annual-assembly/
│   ├── annual-assembly-routing.module.ts
│   ├── annual-assembly.module.ts
│   ├── components/
│   │   ├── list/
│   │   │   ├── annual-assembly-list.component.ts
│   │   │   ├── annual-assembly-list.component.html
│   │   │   └── annual-assembly-list.component.scss
│   │   └── detail/
│   │       ├── annual-assembly-detail.component.ts
│   │       ├── annual-assembly-detail.component.html
│   │       ├── annual-assembly-detail.component.scss
│   │       └── grids/
│   │           ├── session-orders-grid.component.ts
│   │           ├── shareholders-grid.component.ts
│   │           ├── assembly-board-members-grid.component.ts
│   │           ├── inspectors-grid.component.ts
│   │           ├── new-board-members-grid.component.ts
│   │           ├── board-member-wages-grid.component.ts
│   │           ├── newspapers-grid.component.ts
│   │           ├── assembly-interims-grid.component.ts
│   │           └── proportioned-retained-earnings-grid.component.ts
│   ├── services/
│   │   └── annual-assembly.service.ts
│   └── models/
│       ├── annual-assembly-list-item.model.ts
│       ├── annual-assembly-detail.model.ts
│       ├── session-order.model.ts
│       ├── shareholder.model.ts
│       ├── assembly-board-member.model.ts
│       ├── inspector.model.ts
│       ├── new-board-member.model.ts
│       ├── board-member-wage-and-gift.model.ts
│       ├── newspaper.model.ts
│       ├── assembly-interim.model.ts
│       ├── proportioned-retained-earning.model.ts
│       ├── assembly-attendee.model.ts
│       └── enums/
│           ├── session-order-type.enum.ts
│           ├── legal-company-type.enum.ts
│           ├── board-membership-type.enum.ts
│           ├── board-position.enum.ts
│           ├── verification-status.enum.ts
│           ├── inspector-type.enum.ts
│           ├── wage-and-gift-field-type.enum.ts
│           └── proportioned-retained-earning-field-name.enum.ts
```

## Angular Service Implementation

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnnualAssemblyService {
  private readonly baseUrl = '/api/v1/Manufacturing/annual-assembly';

  constructor(private http: HttpClient) {}

  getList(
    isin?: string,
    fiscalYear?: number,
    reportMonth?: number,
    pageNumber: number = 1,
    pageSize: number = 10
  ): Observable<Paginated<AnnualAssemblyListItem>> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    if (isin) {
      params = params.set('Isin', isin);
    }
    if (fiscalYear) {
      params = params.set('FiscalYear', fiscalYear.toString());
    }
    if (reportMonth) {
      params = params.set('ReportMonth', reportMonth.toString());
    }

    return this.http.get<Paginated<AnnualAssemblyListItem>>(
      this.baseUrl,
      { params }
    );
  }

  getById(id: string): Observable<AnnualAssemblyDetail> {
    return this.http.get<AnnualAssemblyDetail>(
      `${this.baseUrl}/${id}`
    );
  }
}
```

## List View Implementation

### Component Template (HTML)
```html
<div class="annual-assembly-list" dir="rtl">
  <!-- Filters -->
  <div class="filters-section">
    <mat-card>
      <mat-card-content>
        <form [formGroup]="filterForm" class="filter-form">
          <mat-form-field appearance="outline">
            <mat-label>نماد (ISIN)</mat-label>
            <input matInput formControlName="isin" placeholder="IRO1BAHN0001">
          </mat-form-field>

          <mat-form-field appearance="outline">
            <mat-label>سال مالی</mat-label>
            <input matInput type="number" formControlName="fiscalYear" placeholder="1404">
          </mat-form-field>

          <mat-form-field appearance="outline">
            <mat-label>ماه گزارش</mat-label>
            <mat-select formControlName="reportMonth">
              <mat-option [value]="null">همه</mat-option>
              <mat-option *ngFor="let month of months" [value]="month.value">
                {{ month.label }}
              </mat-option>
            </mat-select>
          </mat-form-field>

          <button mat-raised-button color="primary" (click)="applyFilters()">
            جستجو
          </button>
          <button mat-button (click)="clearFilters()">
            پاک کردن فیلترها
          </button>
        </form>
      </mat-card-content>
    </mat-card>
  </div>

  <!-- Data Table -->
  <div class="table-section">
    <mat-card>
      <mat-card-content>
        <table mat-table [dataSource]="dataSource" class="persian-table">
          <!-- Symbol Column -->
          <ng-container matColumnDef="symbol">
            <th mat-header-cell *matHeaderCellDef>نماد</th>
            <td mat-cell *matCellDef="let item">{{ item.symbol }}</td>
          </ng-container>

          <!-- ISIN Column -->
          <ng-container matColumnDef="isin">
            <th mat-header-cell *matHeaderCellDef>ISIN</th>
            <td mat-cell *matCellDef="let item">{{ item.isin }}</td>
          </ng-container>

          <!-- Fiscal Year Column -->
          <ng-container matColumnDef="fiscalYear">
            <th mat-header-cell *matHeaderCellDef>سال مالی</th>
            <td mat-cell *matCellDef="let item">{{ item.fiscalYear | persianNumber }}</td>
          </ng-container>

          <!-- Assembly Date Column -->
          <ng-container matColumnDef="assemblyDate">
            <th mat-header-cell *matHeaderCellDef>تاریخ مجمع</th>
            <td mat-cell *matCellDef="let item">{{ item.assemblyDate | jalali | persianNumber }}</td>
          </ng-container>

          <!-- Assembly Result Column -->
          <ng-container matColumnDef="assemblyResult">
            <th mat-header-cell *matHeaderCellDef>نتیجه مجمع</th>
            <td mat-cell *matCellDef="let item">{{ item.assemblyResultTypeTitle }}</td>
          </ng-container>

          <!-- Publish Date Column -->
          <ng-container matColumnDef="publishDate">
            <th mat-header-cell *matHeaderCellDef>تاریخ انتشار</th>
            <td mat-cell *matCellDef="let item">{{ item.publishDate | jalali | persianNumber }}</td>
          </ng-container>

          <!-- Version Column -->
          <ng-container matColumnDef="version">
            <th mat-header-cell *matHeaderCellDef>نسخه</th>
            <td mat-cell *matCellDef="let item">{{ item.version }}</td>
          </ng-container>

          <!-- Actions Column -->
          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>عملیات</th>
            <td mat-cell *matCellDef="let item">
              <button mat-icon-button color="primary" 
                      [routerLink]="['/codal/annual-assembly', item.id]">
                <mat-icon>visibility</mat-icon>
              </button>
              <a mat-icon-button [href]="item.htmlUrl" target="_blank" 
                 matTooltip="مشاهده در سایت CODAL">
                <mat-icon>open_in_new</mat-icon>
              </a>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>

        <!-- Paginator -->
        <mat-paginator
          [length]="totalCount"
          [pageSize]="pageSize"
          [pageSizeOptions]="[10, 25, 50, 100]"
          (page)="onPageChange($event)"
          showFirstLastButtons
          dir="rtl">
        </mat-paginator>
      </mat-card-content>
    </mat-card>
  </div>
</div>
```

### Component TypeScript
```typescript
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-annual-assembly-list',
  templateUrl: './annual-assembly-list.component.html',
  styleUrls: ['./annual-assembly-list.component.scss']
})
export class AnnualAssemblyListComponent implements OnInit {
  filterForm: FormGroup;
  dataSource = new MatTableDataSource<AnnualAssemblyListItem>();
  displayedColumns = ['symbol', 'isin', 'fiscalYear', 'assemblyDate', 'assemblyResult', 'publishDate', 'version', 'actions'];
  
  pageNumber = 1;
  pageSize = 10;
  totalCount = 0;

  months = [
    { value: 1, label: 'فروردین' },
    { value: 2, label: 'اردیبهشت' },
    { value: 3, label: 'خرداد' },
    { value: 4, label: 'تیر' },
    { value: 5, label: 'مرداد' },
    { value: 6, label: 'شهریور' },
    { value: 7, label: 'مهر' },
    { value: 8, label: 'آبان' },
    { value: 9, label: 'آذر' },
    { value: 10, label: 'دی' },
    { value: 11, label: 'بهمن' },
    { value: 12, label: 'اسفند' }
  ];

  constructor(
    private fb: FormBuilder,
    private service: AnnualAssemblyService
  ) {
    this.filterForm = this.fb.group({
      isin: [null],
      fiscalYear: [null],
      reportMonth: [null]
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    const filters = this.filterForm.value;
    this.service.getList(
      filters.isin,
      filters.fiscalYear,
      filters.reportMonth,
      this.pageNumber,
      this.pageSize
    ).subscribe(response => {
      this.dataSource.data = response.items;
      this.totalCount = response.totalCount;
    });
  }

  applyFilters(): void {
    this.pageNumber = 1; // Reset to first page
    this.loadData();
  }

  clearFilters(): void {
    this.filterForm.reset();
    this.pageNumber = 1;
    this.loadData();
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }
}
```

### Component SCSS
```scss
.annual-assembly-list {
  padding: 20px;
  
  .filters-section {
    margin-bottom: 20px;
    
    .filter-form {
      display: flex;
      gap: 16px;
      align-items: center;
      flex-wrap: wrap;
      
      mat-form-field {
        min-width: 200px;
      }
    }
  }
  
  .persian-table {
    width: 100%;
    direction: rtl;
    
    th, td {
      text-align: right;
      padding: 12px 16px;
    }
    
    th {
      font-weight: bold;
      background-color: #f5f5f5;
    }
  }
}
```

## Detail View Implementation

### Component Template (HTML)
```html
<div class="annual-assembly-detail" dir="rtl" *ngIf="detail">
  <!-- Header Section -->
  <mat-card class="header-card">
    <mat-card-header>
      <mat-card-title>تصمیمات مجمع عمومی عادی سالیانه</mat-card-title>
      <mat-card-subtitle>{{ detail.symbol }} - {{ detail.isin }}</mat-card-subtitle>
    </mat-card-header>
    <mat-card-content>
      <div class="header-info">
        <div class="info-row">
          <span class="label">عنوان شرکت:</span>
          <span class="value">{{ detail.title }}</span>
        </div>
        <div class="info-row">
          <span class="label">سال مالی:</span>
          <span class="value">{{ detail.fiscalYear | persianNumber }}</span>
        </div>
        <div class="info-row">
          <span class="label">تاریخ مجمع:</span>
          <span class="value">{{ detail.assemblyDate | jalali | persianNumber }}</span>
        </div>
        <div class="info-row">
          <span class="label">نتیجه مجمع:</span>
          <span class="value">{{ detail.assemblyResultTypeTitle }}</span>
        </div>
        <div class="info-row">
          <span class="label">ساعت:</span>
          <span class="value">{{ detail.assemblyHour || '-' }}</span>
        </div>
        <div class="info-row">
          <span class="label">محل:</span>
          <span class="value">{{ detail.assemblyLocation || '-' }}</span>
        </div>
        <div class="info-row">
          <span class="label">رئیس مجمع:</span>
          <span class="value">{{ detail.assemblyChief || '-' }}</span>
        </div>
        <div class="info-row">
          <span class="label">ناظر اول:</span>
          <span class="value">{{ detail.assemblySuperVisor1 || '-' }}</span>
        </div>
        <div class="info-row">
          <span class="label">ناظر دوم:</span>
          <span class="value">{{ detail.assemblySuperVisor2 || '-' }}</span>
        </div>
        <div class="info-row">
          <span class="label">منشی:</span>
          <span class="value">{{ detail.assemblySecretary || '-' }}</span>
        </div>
        <div class="info-row">
          <span class="label">تاریخ انتشار:</span>
          <span class="value">{{ detail.publishDate | jalali | persianNumber }}</span>
        </div>
        <div class="info-row">
          <span class="label">شماره ردیابی:</span>
          <span class="value">{{ detail.traceNo | persianNumber }}</span>
        </div>
      </div>
      
      <div class="actions">
        <a mat-raised-button color="primary" [href]="detail.htmlUrl" target="_blank">
          <mat-icon>open_in_new</mat-icon>
          مشاهده در CODAL
        </a>
      </div>
    </mat-card-content>
  </mat-card>

  <!-- Tabs for Different Sections -->
  <mat-tab-group class="detail-tabs">
    <!-- Session Orders Tab -->
    <mat-tab label="دستور جلسات">
      <div class="tab-content">
        <app-session-orders-grid [data]="detail.sessionOrders"></app-session-orders-grid>
      </div>
    </mat-tab>

    <!-- Shareholders Tab -->
    <mat-tab label="سهامداران حاضر">
      <div class="tab-content">
        <app-shareholders-grid [data]="detail.shareHolders"></app-shareholders-grid>
      </div>
    </mat-tab>

    <!-- Assembly Board Members Tab -->
    <mat-tab label="هیئت رئیسه مجمع">
      <div class="tab-content">
        <app-assembly-board-members-grid [data]="detail.assemblyBoardMembers"></app-assembly-board-members-grid>
      </div>
    </mat-tab>

    <!-- Inspectors Tab -->
    <mat-tab label="بازرسان">
      <div class="tab-content">
        <app-inspectors-grid [data]="detail.inspectors"></app-inspectors-grid>
      </div>
    </mat-tab>

    <!-- New Board Members Tab -->
    <mat-tab label="اعضای جدید هیئت مدیره">
      <div class="tab-content">
        <app-new-board-members-grid [data]="detail.newBoardMembers"></app-new-board-members-grid>
      </div>
    </mat-tab>

    <!-- Board Member Wages Tab -->
    <mat-tab label="حق الزحمه و هدایا">
      <div class="tab-content">
        <app-board-member-wages-grid [data]="detail.boardMemberWageAndGifts"></app-board-member-wages-grid>
      </div>
    </mat-tab>

    <!-- Newspapers Tab -->
    <mat-tab label="روزنامه‌های کثیرالانتشار">
      <div class="tab-content">
        <app-newspapers-grid [data]="detail.newsPapers"></app-newspapers-grid>
      </div>
    </mat-tab>

    <!-- Assembly Interims Tab -->
    <mat-tab label="اقلام میان دوره‌ای">
      <div class="tab-content">
        <app-assembly-interims-grid [data]="detail.assemblyInterims"></app-assembly-interims-grid>
      </div>
    </mat-tab>

    <!-- Proportioned Retained Earnings Tab -->
    <mat-tab label="تقسیم سود انباشته">
      <div class="tab-content">
        <app-proportioned-retained-earnings-grid [data]="detail.proportionedRetainedEarnings"></app-proportioned-retained-earnings-grid>
      </div>
    </mat-tab>

    <!-- Key Attendees Tab -->
    <mat-tab label="حضور افراد خاص">
      <div class="tab-content">
        <mat-card *ngIf="detail.ceo">
          <mat-card-header>
            <mat-card-title>مدیرعامل</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="attendee-info">
              <div class="info-row">
                <span class="label">نام و نام خانوادگی:</span>
                <span class="value">{{ detail.ceo.fullName || '-' }}</span>
              </div>
              <div class="info-row">
                <span class="label">کد ملی:</span>
                <span class="value">{{ detail.ceo.nationalCode | persianNumber }}</span>
              </div>
              <div class="info-row">
                <span class="label">حضور در جلسه:</span>
                <span class="value">{{ detail.ceo.attendingMeeting ? 'بله' : 'خیر' }}</span>
              </div>
              <div class="info-row">
                <span class="label">مدرک تحصیلی:</span>
                <span class="value">{{ detail.ceo.degree || '-' }}</span>
              </div>
              <div class="info-row">
                <span class="label">رشته تحصیلی:</span>
                <span class="value">{{ detail.ceo.educationField || '-' }}</span>
              </div>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card *ngIf="detail.auditCommitteeChairman">
          <mat-card-header>
            <mat-card-title>رئیس کمیته حسابرسی</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="attendee-info">
              <div class="info-row">
                <span class="label">نام و نام خانوادگی:</span>
                <span class="value">{{ detail.auditCommitteeChairman.fullName || '-' }}</span>
              </div>
              <div class="info-row">
                <span class="label">کد ملی:</span>
                <span class="value">{{ detail.auditCommitteeChairman.nationalCode | persianNumber }}</span>
              </div>
              <div class="info-row">
                <span class="label">حضور در جلسه:</span>
                <span class="value">{{ detail.auditCommitteeChairman.attendingMeeting ? 'بله' : 'خیر' }}</span>
              </div>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card *ngIf="detail.independentAuditorRepresentative">
          <mat-card-header>
            <mat-card-title>نماینده حسابرس مستقل</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="attendee-info">
              <div class="info-row">
                <span class="label">نام و نام خانوادگی:</span>
                <span class="value">{{ detail.independentAuditorRepresentative.fullName || '-' }}</span>
              </div>
              <div class="info-row">
                <span class="label">کد ملی:</span>
                <span class="value">{{ detail.independentAuditorRepresentative.nationalCode | persianNumber }}</span>
              </div>
              <div class="info-row">
                <span class="label">حضور در جلسه:</span>
                <span class="value">{{ detail.independentAuditorRepresentative.attendingMeeting ? 'بله' : 'خیر' }}</span>
              </div>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card *ngIf="detail.topFinancialPosition">
          <mat-card-header>
            <mat-card-title>مقام مالی ارشد</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="attendee-info">
              <div class="info-row">
                <span class="label">نام و نام خانوادگی:</span>
                <span class="value">{{ detail.topFinancialPosition.fullName || '-' }}</span>
              </div>
              <div class="info-row">
                <span class="label">کد ملی:</span>
                <span class="value">{{ detail.topFinancialPosition.nationalCode | persianNumber }}</span>
              </div>
              <div class="info-row">
                <span class="label">حضور در جلسه:</span>
                <span class="value">{{ detail.topFinancialPosition.attendingMeeting ? 'بله' : 'خیر' }}</span>
              </div>
            </div>
          </mat-card-content>
        </mat-card>
      </div>
    </mat-tab>

    <!-- Additional Information Tab -->
    <mat-tab label="سایر اطلاعات">
      <div class="tab-content">
        <mat-card>
          <mat-card-content>
            <div class="info-row" *ngIf="detail.boardMemberPeriod">
              <span class="label">دوره عضویت هیئت مدیره:</span>
              <span class="value">{{ detail.boardMemberPeriod }}</span>
            </div>
            <div class="info-row" *ngIf="detail.publishSecurityDescription">
              <span class="label">توضیحات انتشار اطلاعیه:</span>
              <span class="value">{{ detail.publishSecurityDescription }}</span>
            </div>
            <div class="info-row" *ngIf="detail.otherDescription">
              <span class="label">سایر توضیحات:</span>
              <span class="value">{{ detail.otherDescription }}</span>
            </div>
            <div class="info-row" *ngIf="detail.breakDescription">
              <span class="label">توضیحات تعطیلی:</span>
              <span class="value">{{ detail.breakDescription }}</span>
            </div>
            <div class="info-row" *ngIf="detail.newDate">
              <span class="label">تاریخ جدید:</span>
              <span class="value">{{ detail.newDate }}</span>
            </div>
            <div class="info-row" *ngIf="detail.newLocation">
              <span class="label">محل جدید:</span>
              <span class="value">{{ detail.newLocation }}</span>
            </div>
            <div class="info-row" *ngIf="detail.newDay">
              <span class="label">روز جدید:</span>
              <span class="value">{{ detail.newDay }}</span>
            </div>
            <div class="info-row" *ngIf="detail.newHour">
              <span class="label">ساعت جدید:</span>
              <span class="value">{{ detail.newHour }}</span>
            </div>
          </mat-card-content>
        </mat-card>
      </div>
    </mat-tab>
  </mat-tab-group>
</div>
```

### Component TypeScript
```typescript
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-annual-assembly-detail',
  templateUrl: './annual-assembly-detail.component.html',
  styleUrls: ['./annual-assembly-detail.component.scss']
})
export class AnnualAssemblyDetailComponent implements OnInit {
  detail: AnnualAssemblyDetail | null = null;

  constructor(
    private route: ActivatedRoute,
    private service: AnnualAssemblyService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadDetail(id);
    }
  }

  loadDetail(id: string): void {
    this.service.getById(id).subscribe(detail => {
      this.detail = detail;
    });
  }
}
```

## Example Grid Components

### Session Orders Grid
```typescript
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-session-orders-grid',
  template: `
    <table mat-table [dataSource]="data" class="persian-table">
      <ng-container matColumnDef="type">
        <th mat-header-cell *matHeaderCellDef>نوع</th>
        <td mat-cell *matCellDef="let item">{{ getSessionOrderTypeLabel(item.type) }}</td>
      </ng-container>

      <ng-container matColumnDef="description">
        <th mat-header-cell *matHeaderCellDef>توضیحات</th>
        <td mat-cell *matCellDef="let item">{{ item.description || '-' }}</td>
      </ng-container>

      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  `
})
export class SessionOrdersGridComponent {
  @Input() data: SessionOrder[] = [];
  displayedColumns = ['type', 'description'];

  getSessionOrderTypeLabel(type: SessionOrderType): string {
    const labels: Record<SessionOrderType, string> = {
      [SessionOrderType.BalanceSheetApproval]: 'تصویب ترازنامه',
      [SessionOrderType.IncomeStatementApproval]: 'تصویب صورت سود و زیان',
      [SessionOrderType.CashFlowStatementApproval]: 'تصویب صورت جریان وجوه نقد',
      [SessionOrderType.BoardReportApproval]: 'تصویب گزارش هیئت مدیره',
      [SessionOrderType.AuditorReportApproval]: 'تصویب گزارش بازرس',
      [SessionOrderType.BoardMemberElection]: 'انتخاب اعضای هیئت مدیره',
      [SessionOrderType.InspectorElection]: 'انتخاب بازرس',
      [SessionOrderType.OfficialNewspaperSelection]: 'انتخاب روزنامه کثیرالانتشار',
      [SessionOrderType.ProfitDistribution]: 'تصویب نحوه تقسیم سود',
      [SessionOrderType.CapitalIncrease]: 'افزایش سرمایه',
      [SessionOrderType.Other]: 'سایر'
    };
    return labels[type] || 'نامشخص';
  }
}
```

### Shareholders Grid
```typescript
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-shareholders-grid',
  template: `
    <table mat-table [dataSource]="data" class="persian-table">
      <ng-container matColumnDef="serial">
        <th mat-header-cell *matHeaderCellDef>ردیف</th>
        <td mat-cell *matCellDef="let item">{{ item.shareHolderSerial | persianNumber }}</td>
      </ng-container>

      <ng-container matColumnDef="name">
        <th mat-header-cell *matHeaderCellDef>نام سهامدار</th>
        <td mat-cell *matCellDef="let item">{{ item.name || '-' }}</td>
      </ng-container>

      <ng-container matColumnDef="shareCount">
        <th mat-header-cell *matHeaderCellDef>تعداد سهام</th>
        <td mat-cell *matCellDef="let item">{{ item.shareCount | number | persianNumber }}</td>
      </ng-container>

      <ng-container matColumnDef="sharePercent">
        <th mat-header-cell *matHeaderCellDef>درصد سهام</th>
        <td mat-cell *matCellDef="let item">{{ item.sharePercent | number:'1.2-2' | persianNumber }}%</td>
      </ng-container>

      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  `
})
export class ShareholdersGridComponent {
  @Input() data: ShareHolder[] = [];
  displayedColumns = ['serial', 'name', 'shareCount', 'sharePercent'];
}
```

### Proportioned Retained Earnings Grid
```typescript
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-proportioned-retained-earnings-grid',
  template: `
    <table mat-table [dataSource]="data" class="persian-table financial-table">
      <ng-container matColumnDef="description">
        <th mat-header-cell *matHeaderCellDef>شرح</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)">
          {{ item.description || getFieldNameLabel(item.fieldName) }}
        </td>
      </ng-container>

      <ng-container matColumnDef="value">
        <th mat-header-cell *matHeaderCellDef>مبلغ (ریال)</th>
        <td mat-cell *matCellDef="let item" class="numeric" [class.summary-row]="isSummaryRow(item)">
          {{ item.yearEndToDateValue | number | persianNumber }}
        </td>
      </ng-container>

      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  `
})
export class ProportionedRetainedEarningsGridComponent {
  @Input() data: ProportionedRetainedEarning[] = [];
  displayedColumns = ['description', 'value'];

  isSummaryRow(item: ProportionedRetainedEarning): boolean {
    return item.fieldName === ProportionedRetainedEarningFieldName.TotalEndingRetainedEarnings ||
           item.fieldName === ProportionedRetainedEarningFieldName.ProportionableRetainedEarnings;
  }

  getFieldNameLabel(fieldName: ProportionedRetainedEarningFieldName | null): string {
    if (!fieldName) return '-';
    
    const labels: Record<ProportionedRetainedEarningFieldName, string> = {
      [ProportionedRetainedEarningFieldName.NetIncomeLoss]: 'سود (زیان) خالص',
      [ProportionedRetainedEarningFieldName.BeginingRetainedEarnings]: 'سود (زیان) انباشته ابتدای دوره',
      [ProportionedRetainedEarningFieldName.AnnualAdjustment]: 'تعدیلات سنواتی',
      [ProportionedRetainedEarningFieldName.AdjustedBeginingRetainedEarnings]: 'سود (زیان) انباشته ابتدای دوره تعدیل‌شده',
      [ProportionedRetainedEarningFieldName.PreYearDevidedRetainedEarning]: 'سود سهام مصوب (مجمع سال قبل)',
      [ProportionedRetainedEarningFieldName.TransferToCapital]: 'تغییرات سرمایه از محل سود (زیان) انباشته',
      [ProportionedRetainedEarningFieldName.UnallocatedRetainedEarningsAtTheBeginningOfPeriod]: 'سود انباشته ابتدای دوره تخصیص نیافته',
      [ProportionedRetainedEarningFieldName.TransfersFromOtherEquityItems]: 'انتقال از سایر اقلام حقوق صاحبان سهام',
      [ProportionedRetainedEarningFieldName.ProportionableRetainedEarnings]: 'سود قابل تخصیص',
      [ProportionedRetainedEarningFieldName.LegalReserve]: 'انتقال به اندوخته‌ قانونی',
      [ProportionedRetainedEarningFieldName.ExtenseReserve]: 'انتقال به سایر اندوخته‌ها',
      [ProportionedRetainedEarningFieldName.EndingRetainedEarnings]: 'سود (زیان) انباشته پايان دوره',
      [ProportionedRetainedEarningFieldName.DividedRetainedEarning]: 'سود سهام مصوب (مجمع سال جاری)',
      [ProportionedRetainedEarningFieldName.TotalEndingRetainedEarnings]: 'سود (زیان) انباشته پایان دوره (با لحاظ نمودن مصوبات مجمع)',
      [ProportionedRetainedEarningFieldName.EarningsPerShareAfterTax]: 'سود (زیان) خالص هر سهم- ریال',
      [ProportionedRetainedEarningFieldName.DividendPerShare]: 'سود نقدی هر سهم (ریال)',
      [ProportionedRetainedEarningFieldName.ListedCapital]: 'سرمایه'
    };
    return labels[fieldName] || 'نامشخص';
  }
}
```

## Styling Recommendations

### Global SCSS for Annual Assembly
```scss
.annual-assembly-detail {
  padding: 20px;
  
  .header-card {
    margin-bottom: 20px;
    
    .header-info {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 12px;
      margin-top: 16px;
      
      .info-row {
        display: flex;
        justify-content: space-between;
        padding: 8px;
        border-bottom: 1px solid #e0e0e0;
        
        .label {
          font-weight: 500;
          color: #666;
        }
        
        .value {
          font-weight: 400;
          color: #000;
        }
      }
    }
    
    .actions {
      margin-top: 16px;
      display: flex;
      gap: 12px;
    }
  }
  
  .detail-tabs {
    .tab-content {
      padding: 20px;
    }
  }
  
  .persian-table {
    width: 100%;
    direction: rtl;
    
    th, td {
      text-align: right;
      padding: 12px 16px;
      
      &.numeric {
        text-align: left;
        font-family: 'IRANSans', sans-serif;
      }
    }
    
    th {
      font-weight: bold;
      background-color: #f5f5f5;
    }
    
    .summary-row {
      font-weight: bold;
      background-color: #e3f2fd;
    }
  }
  
  .financial-table {
    .summary-row {
      background-color: #fff3e0;
      border-top: 2px solid #ff9800;
      border-bottom: 2px solid #ff9800;
    }
  }
  
  .attendee-info {
    .info-row {
      display: flex;
      justify-content: space-between;
      padding: 8px 0;
      border-bottom: 1px solid #e0e0e0;
      
      &:last-child {
        border-bottom: none;
      }
    }
  }
}
```

## Utility Services & Pipes

### Persian Number Pipe
```typescript
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'persianNumber' })
export class PersianNumberPipe implements PipeTransform {
  transform(value: any): string {
    if (value === null || value === undefined) return '-';
    
    const str = value.toString();
    const persianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    
    return str.replace(/\d/g, (digit: string) => persianDigits[parseInt(digit)]);
  }
}
```

### Jalali Date Pipe
```typescript
import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment-jalaali';

@Pipe({ name: 'jalali' })
export class JalaliPipe implements PipeTransform {
  transform(value: string | Date, format: string = 'jYYYY/jMM/jDD'): string {
    if (!value) return '-';
    return moment(value).format(format);
  }
}
```

## Error Handling

```typescript
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  constructor(private snackBar: MatSnackBar) {}

  handleError(error: HttpErrorResponse): void {
    let message = 'خطای ناشناخته رخ داده است';
    
    if (error.error?.errorCode === 13_374_101) {
      message = 'تصمیمات مجمع عمومی عادی سالیانه یافت نشد.';
    } else if (error.status === 400) {
      message = 'درخواست نامعتبر است';
    } else if (error.status === 500) {
      message = 'خطای سرور رخ داده است';
    }
    
    this.snackBar.open(message, 'بستن', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      direction: 'rtl'
    });
  }
}
```

## Key Implementation Notes

1. **Enum Labels**: Create helper functions or services to convert enum values to Persian labels
2. **Null Safety**: Use null-conditional operators and provide default values ('-') for nullable fields
3. **Number Formatting**: Always format numbers and percentages with Persian digits
4. **Date Display**: Convert ISO dates to Jalali (Persian) calendar
5. **Grid Styling**: Use different styling for summary rows vs data rows in financial tables
6. **RTL Layout**: Ensure all components support right-to-left layout
7. **Loading States**: Add loading spinners for async operations
8. **Empty States**: Show appropriate messages when collections are empty
9. **Responsive Design**: Make grids responsive for mobile devices
10. **Accessibility**: Add proper ARIA labels for screen readers

This guide provides your Angular development team with everything needed to implement comprehensive Annual Assembly viewing functionality with proper Persian/RTL support and type-safe enum handling.

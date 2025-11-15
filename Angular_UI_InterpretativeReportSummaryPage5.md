# Angular UI Implementation Guide - Interpretative Report Summary Page 5

## Overview

This guide provides detailed instructions for implementing the Angular UI for displaying Interpretative Report Summary Page 5 data from Iranian financial statements (CODAL). The implementation includes:

- **List View**: Filterable, paginated table of reports
- **Detail View**: Comprehensive display with 6 data grids showing financial details
- **Persian/RTL Support**: Right-to-left layout with Persian numbers
- **Row Type Handling**: Separation of data rows vs. summary rows with special styling

## API Endpoints

### Base URL
```
/api/v1/Manufacturing/interpretative-report-summary-page5
```

### 1. Get List (Paginated)
```typescript
GET /api/v1/Manufacturing/interpretative-report-summary-page5
Query Parameters:
  - Isin?: string (e.g., "IRO1MSMI0001")
  - FiscalYear?: number (e.g., 1404)
  - ReportMonth?: number (1-12)
  - PageNumber: number (default: 1)
  - PageSize: number (default: 10, max: 100)

Response: Paginated<InterpretativeReportSummaryPage5ListItem>
```

### 2. Get Detail by ID
```typescript
GET /api/v1/Manufacturing/interpretative-report-summary-page5/{id}
Path Parameters:
  - id: string (GUID)

Response: InterpretativeReportSummaryPage5Detail
Error Codes:
  - 13_400_101: "گزیده گزارش تفسیری صفحه 5 یافت نشد." (Not Found)
```

## TypeScript Data Structures

### List Item Interface
```typescript
export interface InterpretativeReportSummaryPage5ListItem {
  id: string; // GUID
  isin: string; // Symbol ISIN code (e.g., "IRO1MSMI0001")
  symbol: string; // Symbol code (e.g., "خودرو")
  title: string; // Report title
  uri: string; // CODAL report URI
  version: number; // JSON version (2-5)
  fiscalYear: number; // e.g., 1404
  yearEndMonth: number; // 1-12
  reportMonth: number; // 1-12
  traceNo: number; // CODAL trace number
  publishDate: string; // ISO 8601 date-time
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
export interface InterpretativeReportSummaryPage5Detail {
  // Header Information
  id: string;
  isin: string;
  symbol: string;
  title: string;
  uri: string;
  version: number;
  fiscalYear: number;
  yearEndMonth: number;
  reportMonth: number;
  traceNo: number;
  publishDate: string;

  // Financial Data Collections (6 grids)
  otherOperatingIncomes: OtherOperatingIncomeItem[];
  otherNonOperatingExpenses: OtherNonOperatingExpenseItem[];
  financingDetails: FinancingDetailItem[];
  financingDetailsEstimated: FinancingDetailItem[];
  investmentIncomes: InvestmentIncomeItem[];
  miscellaneousExpenses: MiscellaneousExpenseItem[];
  descriptions: InterpretativeReportDescription[];
}
```

### Grid Data Interfaces

#### 1. Other Operating Incomes
```typescript
export interface OtherOperatingIncomeItem {
  rowCode: OtherOperatingIncomeCode; // -1 for data rows, positive for summary
  category: number; // Grouping category
  rowType: RowType; // Data = -1, Summary = other values
  id: number; // Row identifier
  itemDescription: string; // Persian description
  currentPeriodAmount: number; // مبالغ دوره جاری
  lastYearSamePeriodAmount: number; // مبالغ مدت مشابه سال قبل
  fromStartOfYearToEndOfPeriodAmount: number; // مبالغ از ابتدای سال تا پایان دوره
}

// Row Code Enum (Summary Rows)
export enum OtherOperatingIncomeCode {
  TotalOtherOperatingIncomes = 999 // جمع سایر درآمدهای عملیاتی
}

// Row Type Enum
export enum RowType {
  Data = -1,        // Regular data rows
  Sum = 0,          // Sum rows
  Percentage = 1,   // Percentage rows
  Title = 2,        // Title/header rows
  Other = 3         // Other summary types
}
```

#### 2. Other Non-Operating Expenses
```typescript
export interface OtherNonOperatingExpenseItem {
  rowCode: OtherNonOperatingExpenseCode; // -1 for data rows
  category: number;
  rowType: RowType;
  id: number;
  itemDescription: string;
  currentPeriodAmount: number;
  lastYearSamePeriodAmount: number;
  fromStartOfYearToEndOfPeriodAmount: number;
}

export enum OtherNonOperatingExpenseCode {
  TotalOtherNonOperatingExpenses = 999 // جمع سایر هزینه‌های غیر عملیاتی
}
```

#### 3. Financing Details (Current + Estimated)
```typescript
export interface FinancingDetailItem {
  rowCode: FinancingDetailCode; // -1 for data rows
  category: number;
  rowType: RowType;
  id: number;
  financingSource: string; // منبع تأمین مالی
  interestRate: number; // نرخ سود (%)
  loanBalanceAtStartOfYear: number; // مانده وام ابتدای سال
  loanAmountReceivedDuringPeriod: number; // مبلغ وام دریافتی در طول دوره
  repaidAmountDuringPeriod: number; // مبلغ بازپرداختی در طول دوره
  loanBalanceAtEndOfPeriod: number; // مانده وام پایان دوره
  currencyTypeAtStartOfYear: string; // نوع ارز ابتدای سال
  currencyTypeReceived: string; // نوع ارز دریافتی
  currencyTypeRepaid: string; // نوع ارز بازپرداختی
  currencyTypeAtEndOfPeriod: string; // نوع ارز پایان دوره
  loanRepaymentTerm: string; // مدت بازپرداخت وام
  financialExpense: number; // هزینه مالی
}

export enum FinancingDetailCode {
  TotalFinancingDetails = 999 // جمع
}
```

#### 4. Investment Incomes
```typescript
export interface InvestmentIncomeItem {
  rowCode: InvestmentIncomeCode;
  category: number;
  rowType: RowType;
  id: number;
  itemDescription: string;
  currentPeriodAmount: number;
  lastYearSamePeriodAmount: number;
  fromStartOfYearToEndOfPeriodAmount: number;
}

export enum InvestmentIncomeCode {
  TotalInvestmentIncomes = 999 // جمع درآمدهای سرمایه‌گذاری
}
```

#### 5. Miscellaneous Expenses
```typescript
export interface MiscellaneousExpenseItem {
  rowCode: MiscellaneousExpenseCode;
  category: number;
  rowType: RowType;
  id: number;
  itemDescription: string;
  currentPeriodAmount: number;
  lastYearSamePeriodAmount: number;
  fromStartOfYearToEndOfPeriodAmount: number;
}

export enum MiscellaneousExpenseCode {
  TotalMiscellaneousExpenses = 999 // جمع هزینه‌های متفرقه
}
```

#### 6. Descriptions
```typescript
export interface InterpretativeReportDescription {
  rowCode: number; // Sequential numbering
  category: number;
  rowType: RowType;
  description: string; // Persian description text
  sectionName: string; // Section identifier
  additionalValue1?: string; // Multi-column support
  additionalValue2?: string;
  additionalValue3?: string;
  additionalValue4?: string;
  additionalValue5?: string;
}
```

## Data Row vs. Summary Row Detection

### Critical Pattern
```typescript
export function isDataRow(rowCode: number): boolean {
  return rowCode === -1;
}

export function isSummaryRow(rowCode: number): boolean {
  return rowCode !== -1;
}

// Category-based grouping for data rows
export function groupDataRowsByCategory<T extends { category: number; rowCode: number }>(
  items: T[]
): Map<number, T[]> {
  const dataRows = items.filter(item => isDataRow(item.rowCode));
  return dataRows.reduce((map, item) => {
    const category = item.category;
    if (!map.has(category)) {
      map.set(category, []);
    }
    map.get(category)!.push(item);
    return map;
  }, new Map<number, T[]>());
}

// Get summary row for a specific category
export function getSummaryRowForCategory<T extends { category: number; rowCode: number }>(
  items: T[],
  category: number
): T | undefined {
  return items.find(item => 
    isSummaryRow(item.rowCode) && item.category === category
  );
}
```

### Usage Example
```typescript
// Separate data and summary rows
const dataRows = otherOperatingIncomes.filter(item => isDataRow(item.rowCode));
const summaryRows = otherOperatingIncomes.filter(item => isSummaryRow(item.rowCode));

// Group data rows by category
const groupedByCategory = groupDataRowsByCategory(otherOperatingIncomes);

// Find total summary row
const totalRow = otherOperatingIncomes.find(
  item => item.rowCode === OtherOperatingIncomeCode.TotalOtherOperatingIncomes
);
```

## Component Architecture

### Recommended Structure
```
src/app/codal/
├── interpretative-report-summary-page5/
│   ├── interpretative-report-summary-page5-routing.module.ts
│   ├── interpretative-report-summary-page5.module.ts
│   ├── components/
│   │   ├── list/
│   │   │   ├── interpretative-report-summary-page5-list.component.ts
│   │   │   ├── interpretative-report-summary-page5-list.component.html
│   │   │   └── interpretative-report-summary-page5-list.component.scss
│   │   └── detail/
│   │       ├── interpretative-report-summary-page5-detail.component.ts
│   │       ├── interpretative-report-summary-page5-detail.component.html
│   │       ├── interpretative-report-summary-page5-detail.component.scss
│   │       └── grids/
│   │           ├── other-operating-incomes-grid.component.ts
│   │           ├── other-non-operating-expenses-grid.component.ts
│   │           ├── financing-details-grid.component.ts
│   │           ├── investment-incomes-grid.component.ts
│   │           ├── miscellaneous-expenses-grid.component.ts
│   │           └── descriptions-grid.component.ts
│   ├── services/
│   │   └── interpretative-report-summary-page5.service.ts
│   └── models/
│       ├── interpretative-report-summary-page5-list-item.model.ts
│       ├── interpretative-report-summary-page5-detail.model.ts
│       ├── other-operating-income-item.model.ts
│       ├── other-non-operating-expense-item.model.ts
│       ├── financing-detail-item.model.ts
│       ├── investment-income-item.model.ts
│       ├── miscellaneous-expense-item.model.ts
│       ├── interpretative-report-description.model.ts
│       └── enums/
│           ├── row-type.enum.ts
│           ├── other-operating-income-code.enum.ts
│           ├── other-non-operating-expense-code.enum.ts
│           ├── financing-detail-code.enum.ts
│           ├── investment-income-code.enum.ts
│           └── miscellaneous-expense-code.enum.ts
```

## Angular Service Implementation

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InterpretativeReportSummaryPage5Service {
  private readonly baseUrl = '/api/v1/Manufacturing/interpretative-report-summary-page5';

  constructor(private http: HttpClient) {}

  getList(
    isin?: string,
    fiscalYear?: number,
    reportMonth?: number,
    pageNumber: number = 1,
    pageSize: number = 10
  ): Observable<Paginated<InterpretativeReportSummaryPage5ListItem>> {
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

    return this.http.get<Paginated<InterpretativeReportSummaryPage5ListItem>>(
      this.baseUrl,
      { params }
    );
  }

  getById(id: string): Observable<InterpretativeReportSummaryPage5Detail> {
    return this.http.get<InterpretativeReportSummaryPage5Detail>(
      `${this.baseUrl}/${id}`
    );
  }
}
```

## List View Implementation

### Component Template (HTML)
```html
<div class="interpretative-report-summary-page5-list" dir="rtl">
  <!-- Filters -->
  <div class="filters-section">
    <mat-card>
      <mat-card-content>
        <form [formGroup]="filterForm" class="filter-form">
          <mat-form-field appearance="outline">
            <mat-label>نماد (ISIN)</mat-label>
            <input matInput formControlName="isin" placeholder="IRO1MSMI0001">
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

          <!-- Report Month Column -->
          <ng-container matColumnDef="reportMonth">
            <th mat-header-cell *matHeaderCellDef>ماه گزارش</th>
            <td mat-cell *matCellDef="let item">{{ getMonthName(item.reportMonth) }}</td>
          </ng-container>

          <!-- Publish Date Column -->
          <ng-container matColumnDef="publishDate">
            <th mat-header-cell *matHeaderCellDef>تاریخ انتشار</th>
            <td mat-cell *matCellDef="let item">{{ item.publishDate | jalali | persianNumber }}</td>
          </ng-container>

          <!-- Version Column -->
          <ng-container matColumnDef="version">
            <th mat-header-cell *matHeaderCellDef>نسخه</th>
            <td mat-cell *matCellDef="let item">V{{ item.version }}</td>
          </ng-container>

          <!-- Actions Column -->
          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>عملیات</th>
            <td mat-cell *matCellDef="let item">
              <button mat-icon-button color="primary" 
                      [routerLink]="['/codal/interpretative-report-summary-page5', item.id]">
                <mat-icon>visibility</mat-icon>
              </button>
              <a mat-icon-button [href]="item.uri" target="_blank" 
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
  selector: 'app-interpretative-report-summary-page5-list',
  templateUrl: './interpretative-report-summary-page5-list.component.html',
  styleUrls: ['./interpretative-report-summary-page5-list.component.scss']
})
export class InterpretativeReportSummaryPage5ListComponent implements OnInit {
  filterForm: FormGroup;
  dataSource = new MatTableDataSource<InterpretativeReportSummaryPage5ListItem>();
  displayedColumns = ['symbol', 'isin', 'fiscalYear', 'reportMonth', 'publishDate', 'version', 'actions'];
  
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
    private service: InterpretativeReportSummaryPage5Service
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

  getMonthName(month: number): string {
    return this.months.find(m => m.value === month)?.label || '';
  }
}
```

## Detail View Implementation

### Component Template (HTML)
```html
<div class="interpretative-report-summary-page5-detail" dir="rtl" *ngIf="detail">
  <!-- Header Section -->
  <mat-card class="header-card">
    <mat-card-header>
      <mat-card-title>گزیده گزارش تفسیری صفحه 5</mat-card-title>
      <mat-card-subtitle>{{ detail.symbol }} - {{ detail.isin }}</mat-card-subtitle>
    </mat-card-header>
    <mat-card-content>
      <div class="header-info">
        <div class="info-row">
          <span class="label">سال مالی:</span>
          <span class="value">{{ detail.fiscalYear | persianNumber }}</span>
        </div>
        <div class="info-row">
          <span class="label">ماه گزارش:</span>
          <span class="value">{{ getMonthName(detail.reportMonth) }}</span>
        </div>
        <div class="info-row">
          <span class="label">تاریخ انتشار:</span>
          <span class="value">{{ detail.publishDate | jalali | persianNumber }}</span>
        </div>
        <div class="info-row">
          <span class="label">نسخه:</span>
          <span class="value">V{{ detail.version }}</span>
        </div>
        <div class="info-row">
          <a mat-raised-button [href]="detail.uri" target="_blank">
            <mat-icon>open_in_new</mat-icon>
            مشاهده در CODAL
          </a>
        </div>
      </div>
    </mat-card-content>
  </mat-card>

  <!-- Tabs for Different Grids -->
  <mat-tab-group>
    <!-- Other Operating Incomes -->
    <mat-tab label="سایر درآمدهای عملیاتی">
      <app-other-operating-incomes-grid
        [items]="detail.otherOperatingIncomes">
      </app-other-operating-incomes-grid>
    </mat-tab>

    <!-- Other Non-Operating Expenses -->
    <mat-tab label="سایر هزینه‌های غیر عملیاتی">
      <app-other-non-operating-expenses-grid
        [items]="detail.otherNonOperatingExpenses">
      </app-other-non-operating-expenses-grid>
    </mat-tab>

    <!-- Financing Details -->
    <mat-tab label="جزئیات تأمین مالی">
      <app-financing-details-grid
        [items]="detail.financingDetails"
        [title]="'جزئیات تأمین مالی'">
      </app-financing-details-grid>
    </mat-tab>

    <!-- Financing Details Estimated -->
    <mat-tab label="جزئیات تأمین مالی (برآوردی)">
      <app-financing-details-grid
        [items]="detail.financingDetailsEstimated"
        [title]="'جزئیات تأمین مالی (برآوردی)'">
      </app-financing-details-grid>
    </mat-tab>

    <!-- Investment Incomes -->
    <mat-tab label="درآمدهای سرمایه‌گذاری">
      <app-investment-incomes-grid
        [items]="detail.investmentIncomes">
      </app-investment-incomes-grid>
    </mat-tab>

    <!-- Miscellaneous Expenses -->
    <mat-tab label="هزینه‌های متفرقه">
      <app-miscellaneous-expenses-grid
        [items]="detail.miscellaneousExpenses">
      </app-miscellaneous-expenses-grid>
    </mat-tab>

    <!-- Descriptions -->
    <mat-tab label="توضیحات">
      <app-descriptions-grid
        [items]="detail.descriptions">
      </app-descriptions-grid>
    </mat-tab>
  </mat-tab-group>
</div>
```

### Component TypeScript
```typescript
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-interpretative-report-summary-page5-detail',
  templateUrl: './interpretative-report-summary-page5-detail.component.html',
  styleUrls: ['./interpretative-report-summary-page5-detail.component.scss']
})
export class InterpretativeReportSummaryPage5DetailComponent implements OnInit {
  detail?: InterpretativeReportSummaryPage5Detail;

  constructor(
    private route: ActivatedRoute,
    private service: InterpretativeReportSummaryPage5Service
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.service.getById(id).subscribe(
        data => this.detail = data,
        error => {
          // Handle 13_400_101 error code
          console.error('گزیده گزارش تفسیری صفحه 5 یافت نشد');
        }
      );
    }
  }

  getMonthName(month: number): string {
    const months = ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور',
                    'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'];
    return months[month - 1] || '';
  }
}
```

## Grid Component Example: Other Operating Incomes

### Grid Component Template
```html
<div class="operating-incomes-grid">
  <table mat-table [dataSource]="dataSource" class="financial-grid persian-rtl">
    <!-- Row Description Column -->
    <ng-container matColumnDef="itemDescription">
      <th mat-header-cell *matHeaderCellDef>شرح ردیف</th>
      <td mat-cell *matCellDef="let item" 
          [class.summary-row]="isSummaryRow(item)"
          [class.data-row]="isDataRow(item)">
        {{ item.itemDescription }}
      </td>
    </ng-container>

    <!-- Current Period Amount -->
    <ng-container matColumnDef="currentPeriodAmount">
      <th mat-header-cell *matHeaderCellDef>مبالغ دوره جاری</th>
      <td mat-cell *matCellDef="let item" 
          [class.summary-row]="isSummaryRow(item)"
          class="number-cell">
        {{ item.currentPeriodAmount | number:'1.0-0' | persianNumber }}
      </td>
    </ng-container>

    <!-- Last Year Same Period Amount -->
    <ng-container matColumnDef="lastYearSamePeriodAmount">
      <th mat-header-cell *matHeaderCellDef>مبالغ مدت مشابه سال قبل</th>
      <td mat-cell *matCellDef="let item" 
          [class.summary-row]="isSummaryRow(item)"
          class="number-cell">
        {{ item.lastYearSamePeriodAmount | number:'1.0-0' | persianNumber }}
      </td>
    </ng-container>

    <!-- From Start of Year to End of Period Amount -->
    <ng-container matColumnDef="fromStartOfYearToEndOfPeriodAmount">
      <th mat-header-cell *matHeaderCellDef>مبالغ از ابتدای سال تا پایان دوره</th>
      <td mat-cell *matCellDef="let item" 
          [class.summary-row]="isSummaryRow(item)"
          class="number-cell">
        {{ item.fromStartOfYearToEndOfPeriodAmount | number:'1.0-0' | persianNumber }}
      </td>
    </ng-container>

    <tr mat-header-row *matHeaderRowDef="displayedColumns; sticky: true"></tr>
    <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
  </table>
</div>
```

### Grid Component TypeScript
```typescript
import { Component, Input, OnChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-other-operating-incomes-grid',
  templateUrl: './other-operating-incomes-grid.component.html',
  styleUrls: ['./other-operating-incomes-grid.component.scss']
})
export class OtherOperatingIncomesGridComponent implements OnChanges {
  @Input() items: OtherOperatingIncomeItem[] = [];

  dataSource = new MatTableDataSource<OtherOperatingIncomeItem>();
  displayedColumns = [
    'itemDescription',
    'currentPeriodAmount',
    'lastYearSamePeriodAmount',
    'fromStartOfYearToEndOfPeriodAmount'
  ];

  ngOnChanges(): void {
    if (this.items) {
      // Sort: data rows by category and id, then summary rows
      const sorted = this.sortItems(this.items);
      this.dataSource.data = sorted;
    }
  }

  sortItems(items: OtherOperatingIncomeItem[]): OtherOperatingIncomeItem[] {
    const dataRows = items.filter(item => this.isDataRow(item))
      .sort((a, b) => {
        if (a.category !== b.category) {
          return a.category - b.category;
        }
        return a.id - b.id;
      });

    const summaryRows = items.filter(item => this.isSummaryRow(item))
      .sort((a, b) => a.rowCode - b.rowCode);

    return [...dataRows, ...summaryRows];
  }

  isDataRow(item: OtherOperatingIncomeItem): boolean {
    return item.rowCode === -1;
  }

  isSummaryRow(item: OtherOperatingIncomeItem): boolean {
    return item.rowCode !== -1;
  }
}
```

## Grid Component Example: Financing Details

### Grid Component Template (Wider Columns)
```html
<div class="financing-details-grid">
  <h3 class="grid-title">{{ title }}</h3>
  
  <div class="table-container">
    <table mat-table [dataSource]="dataSource" class="financial-grid persian-rtl wide-grid">
      <!-- Financing Source -->
      <ng-container matColumnDef="financingSource">
        <th mat-header-cell *matHeaderCellDef>منبع تأمین مالی</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)">
          {{ item.financingSource }}
        </td>
      </ng-container>

      <!-- Interest Rate -->
      <ng-container matColumnDef="interestRate">
        <th mat-header-cell *matHeaderCellDef>نرخ سود (%)</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)" class="number-cell">
          {{ item.interestRate | number:'1.2-2' | persianNumber }}
        </td>
      </ng-container>

      <!-- Loan Balance at Start of Year -->
      <ng-container matColumnDef="loanBalanceAtStartOfYear">
        <th mat-header-cell *matHeaderCellDef>مانده وام ابتدای سال</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)" class="number-cell">
          {{ item.loanBalanceAtStartOfYear | number:'1.0-0' | persianNumber }}
        </td>
      </ng-container>

      <!-- Loan Amount Received During Period -->
      <ng-container matColumnDef="loanAmountReceivedDuringPeriod">
        <th mat-header-cell *matHeaderCellDef>مبلغ وام دریافتی در طول دوره</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)" class="number-cell">
          {{ item.loanAmountReceivedDuringPeriod | number:'1.0-0' | persianNumber }}
        </td>
      </ng-container>

      <!-- Repaid Amount During Period -->
      <ng-container matColumnDef="repaidAmountDuringPeriod">
        <th mat-header-cell *matHeaderCellDef>مبلغ بازپرداختی در طول دوره</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)" class="number-cell">
          {{ item.repaidAmountDuringPeriod | number:'1.0-0' | persianNumber }}
        </td>
      </ng-container>

      <!-- Loan Balance at End of Period -->
      <ng-container matColumnDef="loanBalanceAtEndOfPeriod">
        <th mat-header-cell *matHeaderCellDef>مانده وام پایان دوره</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)" class="number-cell">
          {{ item.loanBalanceAtEndOfPeriod | number:'1.0-0' | persianNumber }}
        </td>
      </ng-container>

      <!-- Currency Type at Start -->
      <ng-container matColumnDef="currencyTypeAtStartOfYear">
        <th mat-header-cell *matHeaderCellDef>نوع ارز ابتدای سال</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)">
          {{ item.currencyTypeAtStartOfYear }}
        </td>
      </ng-container>

      <!-- Currency Type Received -->
      <ng-container matColumnDef="currencyTypeReceived">
        <th mat-header-cell *matHeaderCellDef>نوع ارز دریافتی</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)">
          {{ item.currencyTypeReceived }}
        </td>
      </ng-container>

      <!-- Loan Repayment Term -->
      <ng-container matColumnDef="loanRepaymentTerm">
        <th mat-header-cell *matHeaderCellDef>مدت بازپرداخت وام</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)">
          {{ item.loanRepaymentTerm }}
        </td>
      </ng-container>

      <!-- Financial Expense -->
      <ng-container matColumnDef="financialExpense">
        <th mat-header-cell *matHeaderCellDef>هزینه مالی</th>
        <td mat-cell *matCellDef="let item" [class.summary-row]="isSummaryRow(item)" class="number-cell">
          {{ item.financialExpense | number:'1.0-0' | persianNumber }}
        </td>
      </ng-container>

      <tr mat-header-row *matHeaderRowDef="displayedColumns; sticky: true"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  </div>
</div>
```

### Grid Component TypeScript
```typescript
import { Component, Input, OnChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-financing-details-grid',
  templateUrl: './financing-details-grid.component.html',
  styleUrls: ['./financing-details-grid.component.scss']
})
export class FinancingDetailsGridComponent implements OnChanges {
  @Input() items: FinancingDetailItem[] = [];
  @Input() title: string = 'جزئیات تأمین مالی';

  dataSource = new MatTableDataSource<FinancingDetailItem>();
  displayedColumns = [
    'financingSource',
    'interestRate',
    'loanBalanceAtStartOfYear',
    'loanAmountReceivedDuringPeriod',
    'repaidAmountDuringPeriod',
    'loanBalanceAtEndOfPeriod',
    'currencyTypeAtStartOfYear',
    'currencyTypeReceived',
    'loanRepaymentTerm',
    'financialExpense'
  ];

  ngOnChanges(): void {
    if (this.items) {
      const sorted = this.sortItems(this.items);
      this.dataSource.data = sorted;
    }
  }

  sortItems(items: FinancingDetailItem[]): FinancingDetailItem[] {
    const dataRows = items.filter(item => item.rowCode === -1)
      .sort((a, b) => a.category - b.category || a.id - b.id);
    const summaryRows = items.filter(item => item.rowCode !== -1)
      .sort((a, b) => a.rowCode - b.rowCode);
    return [...dataRows, ...summaryRows];
  }

  isSummaryRow(item: FinancingDetailItem): boolean {
    return item.rowCode !== -1;
  }
}
```

## Styling Guidelines (SCSS)

### Persian/RTL Layout
```scss
// Base RTL styling
.interpretative-report-summary-page5-detail,
.interpretative-report-summary-page5-list {
  direction: rtl;
  text-align: right;
  font-family: 'IRANSans', 'Tahoma', sans-serif;
}

// Financial Grid Styling
.financial-grid {
  width: 100%;
  direction: rtl;
  
  // Header styling
  .mat-header-cell {
    font-weight: bold;
    background-color: #f5f5f5;
    border-bottom: 2px solid #ddd;
    padding: 12px 8px;
    text-align: right;
  }
  
  // Data row styling
  .mat-cell {
    padding: 8px;
    border-bottom: 1px solid #eee;
    
    &.data-row {
      background-color: #ffffff;
      font-weight: normal;
    }
    
    // Summary row styling (CODAL convention)
    &.summary-row {
      background-color: #fff9e6; // Light yellow background
      font-weight: bold;
      color: #d9534f; // Red text for emphasis
      border-top: 2px solid #f0ad4e;
      border-bottom: 2px solid #f0ad4e;
    }
  }
  
  // Number cell alignment
  .number-cell {
    text-align: left;
    font-family: 'IRANSans', monospace;
    direction: ltr;
  }
  
  // Sticky header for scrolling
  .mat-header-row {
    position: sticky;
    top: 0;
    z-index: 100;
    background-color: #f5f5f5;
  }
}

// Wide grid for financing details (horizontal scroll)
.wide-grid {
  .table-container {
    overflow-x: auto;
    max-width: 100%;
  }
  
  .mat-cell, .mat-header-cell {
    min-width: 120px;
    white-space: nowrap;
  }
}

// Persian number styling
.persian-number {
  font-family: 'IRANSans', sans-serif;
  direction: ltr;
  display: inline-block;
}

// Card styling
.header-card {
  margin-bottom: 20px;
  
  .header-info {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    gap: 16px;
    
    .info-row {
      display: flex;
      gap: 8px;
      
      .label {
        font-weight: bold;
        color: #666;
      }
      
      .value {
        color: #333;
      }
    }
  }
}

// Tab styling
.mat-tab-group {
  margin-top: 20px;
  
  .mat-tab-label {
    font-family: 'IRANSans', sans-serif;
    font-size: 14px;
  }
}

// Filter form
.filter-form {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 16px;
  align-items: center;
}
```

## Custom Pipes

### Persian Number Pipe
```typescript
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'persianNumber' })
export class PersianNumberPipe implements PipeTransform {
  private persianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];

  transform(value: any): string {
    if (value === null || value === undefined) {
      return '';
    }
    
    return value.toString().replace(/\d/g, (digit: string) => {
      return this.persianDigits[parseInt(digit)];
    });
  }
}
```

### Jalali Date Pipe
```typescript
import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment-jalaali';

@Pipe({ name: 'jalali' })
export class JalaliPipe implements PipeTransform {
  transform(value: string | Date, format: string = 'jYYYY/jMM/jDD HH:mm'): string {
    if (!value) {
      return '';
    }
    return moment(value).format(format);
  }
}
```

## Performance Considerations

### Lazy Loading
```typescript
// In app-routing.module.ts
const routes: Routes = [
  {
    path: 'codal/interpretative-report-summary-page5',
    loadChildren: () => import('./codal/interpretative-report-summary-page5/interpretative-report-summary-page5.module')
      .then(m => m.InterpretativeReportSummaryPage5Module)
  }
];
```

### Virtual Scrolling (for large datasets)
```html
<!-- Use CDK Virtual Scroll for grids with many rows -->
<cdk-virtual-scroll-viewport itemSize="50" class="viewport">
  <table mat-table [dataSource]="dataSource">
    <!-- columns -->
  </table>
</cdk-virtual-scroll-viewport>
```

### Change Detection Strategy
```typescript
import { ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-other-operating-incomes-grid',
  templateUrl: './other-operating-incomes-grid.component.html',
  styleUrls: ['./other-operating-incomes-grid.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush // Improve performance
})
export class OtherOperatingIncomesGridComponent { }
```

## Testing Checklist

- [ ] List view loads with default pagination
- [ ] Filters work correctly (ISIN, fiscal year, report month)
- [ ] Pagination controls work (next, previous, page size change)
- [ ] Persian numbers display correctly
- [ ] Jalali dates display correctly
- [ ] Navigation to detail view works
- [ ] Detail view loads all 6 grids correctly
- [ ] Data rows and summary rows have distinct styling
- [ ] Summary rows display with yellow background and red text
- [ ] All grids sort correctly (data rows by category/id, then summary rows)
- [ ] Horizontal scroll works for financing details wide grid
- [ ] Sticky headers work when scrolling vertically
- [ ] RTL layout is consistent across all views
- [ ] CODAL link opens in new tab
- [ ] Error handling for 13_400_101 (not found) displays Persian message
- [ ] Loading states display appropriately
- [ ] Empty state displays when no data available

## Additional Notes

1. **Row Code Pattern**: The critical distinction is `rowCode === -1` for data rows vs. `rowCode !== -1` for summary/total rows. This pattern is consistent across all 6 collection types.

2. **Category Grouping**: Data rows with the same `category` value belong to the same logical group. Summary rows typically apply to their respective category.

3. **CODAL Color Convention**: Summary rows use light yellow background (`#fff9e6`) with red text (`#d9534f`) and orange borders (`#f0ad4e`) to match CODAL website styling.

4. **Financing Details Uniqueness**: This grid has 14 columns (compared to 3-4 for other grids), requiring horizontal scrolling and wider viewport.

5. **Descriptions Special Case**: The descriptions grid supports multi-column layout with 5 additional value fields, allowing flexible table structures.

6. **Version Information**: The `version` field indicates the JSON schema version (2-5) used by CODAL. Display this to users for transparency.

7. **Persian Localization**: Use `IRANSans` font, convert all numbers to Persian digits, and display Jalali dates. Ensure all UI text is in Persian.

8. **Error Handling**: The API returns error code `13_400_101` with Persian message when a report is not found. Display this gracefully to users.

9. **Performance**: For grids with 100+ rows, implement virtual scrolling using Angular CDK. Use OnPush change detection strategy.

10. **Accessibility**: Ensure tables have proper ARIA labels for screen readers, even though Persian RTL users are the primary audience.

## Example User Flow

1. User navigates to list view
2. User filters by ISIN "IRO1MSMI0001" and fiscal year 1404
3. List displays 10 results with pagination
4. User clicks "مشاهده" (view) icon on a row
5. Detail view opens with 7 tabs
6. User clicks "سایر درآمدهای عملیاتی" tab
7. Grid displays data rows grouped by category, followed by yellow-highlighted summary row
8. User scrolls to "جزئیات تأمین مالی" tab
9. Wide grid displays with horizontal scroll
10. User can export, print, or navigate to CODAL source

This guide should provide your Angular development team with all the necessary information to implement both the list and detail views for Interpretative Report Summary Page 5 data.
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../api/product';
import * as XLSX from 'xlsx';
import { Table } from 'primeng/table';
import * as FileSaver from 'file-saver';

@Injectable({
  providedIn: 'root'
})
export class exportExcelService {
    getProductsMini() {
      throw new Error('Method not implemented.');
    }

    constructor(private http: HttpClient) { }

    getProductsSmall() {
        return this.http.get<any>('assets/demo/data/products-small.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    getProducts() {
        return this.http.get<any>('assets/demo/data/products.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    getProductsMixed() {
        return this.http.get<any>('assets/demo/data/products-mixed.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    getProductsWithOrdersSmall() {
        return this.http.get<any>('assets/demo/data/products-orders-small.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    dateToYearString(date: any): string {
        const newdate = new Date(date);
        return ((newdate.getFullYear().toString()) + '-' + ("0" + (newdate.getMonth() + 1)).slice(-2) + '-' + ("0" + (newdate.getDate())).slice(-2));
      }

exportToExcel( dt1:Table,FileName:String) {
        const data = dt1.value;

        var todaysDate = new Date()
        var year = todaysDate.getFullYear().toString();
        var month = (todaysDate.getMonth() + 1).toString().padStart(2, '0'); // Add 1 to get correct month and pad with zero if needed
        var day = todaysDate.getDate().toString().padStart(2, '0'); // Pad with zero if needed
        var date = year+month+day
      
        // Define the columns you want to include in the export
        const displayedColumnNames = dt1.globalFilterFields
        // Filter and map the data to include only the selected columns
        const filteredData = data.map((item: { [x: string]: any; }) => {
          const newItem: Record<string, any> = {};
          displayedColumnNames.forEach(column => {
            newItem[column] = item[column];
            if (column === 'TargetDate' || column === 'CompletionDate' || column === 'RegulatoryDueDate' || column === 'GistEffectiveStartDate' || column === 'CircularDate' || column ==='RegulatoryDueDate ' || column === 'SubmissionDate') {
              if (item[column] === null) {
      
              }
              else {
                item[column] = this.dateToYearString(new Date(item[column]))
              }
      
            }
          });
          return newItem;
        });
      
        // Convert filtered data to Excel worksheet
        const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(filteredData);
      
        // Set auto width for each column
        const colWidths = displayedColumnNames.map(column => ({ wch: column.length + 10 })); // Adjust the factor as needed
        ws['!cols'] = colWidths;
      
        // Create a workbook with a single sheet
        const wb: XLSX.WorkBook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
      
      
      
        // Generate Excel binary as an ArrayBuffer
        const arrayBuffer = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });
      
        // Convert ArrayBuffer to Blob using a utility function
        const blob = new Blob([arrayBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      
        // Save the Blob as an Excel file
        FileSaver.saveAs(blob, FileName+'_'+date+'.xlsx');
      }

}

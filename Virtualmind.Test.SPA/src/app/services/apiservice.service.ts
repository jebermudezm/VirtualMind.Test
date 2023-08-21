import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl: string = 'https://localhost:7189/api';

  constructor(private http: HttpClient) {}

  getExchangeRate(currencyCode: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/ExchangeRate/${currencyCode}`);
  }

  purchaseCurrency(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Transactions`, data);
  }
}

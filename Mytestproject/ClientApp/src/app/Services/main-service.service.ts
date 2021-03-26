import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { company } from '../Model/Company';
import { country } from '../Model/Country';
import { MainDataTable } from '../Model/MainDataTable';
import { user } from '../Model/User';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  myAppUrl: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }
//for getting countries for dropdown
  getcountries(): Observable<country[]> {
    return this.http.get<country[]>(this.myAppUrl + 'api/Countries');
  }
  //for getting Companies for dropdown
  getcompanies(): Observable<company[]> {
    return this.http.get<company[]>(this.myAppUrl + 'api/Companies');
  }
  //for getting Users for dropdown
  getusers(): Observable<user[]> {
    return this.http.get<user[]>(this.myAppUrl + 'api/Users');
  }
  //for getting Companies based on countries for dropdown
  getcompanyFromCountry(c: any): Observable<company[]> {
    return this.http.get<company[]>(this.myAppUrl + 'api/Companies/bycountries?c=' + c);
  }
  //for getting Users based on companies for dropdown
  getuserFromCompany(c: any): Observable<user[]> {
    return this.http.get<user[]>(this.myAppUrl + 'api/Users/bycompanies?c=' + c);
  }
  //for getting maintable based on dropdowns
  getmainDataTable(country: any, company: any, user: any): Observable<MainDataTable[]> {
    return this.http.get<MainDataTable[]>(this.myAppUrl + 'api/Countries/maintable?country=' + country + '&company=' + company + '&user=' + user);
  }
}

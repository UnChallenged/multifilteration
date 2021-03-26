import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { company } from '../Model/Company';
import { country } from '../Model/Country';
import { user } from '../Model/User';
import { MainServiceService } from '../Services/main-service.service';


@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  public countrylist=[];
  public companylist=[];
  public userlist = [];

  public finalResult = [];
  //paging variable
  first = 0;
  rows = 5;
  ////

  public selectedcountries:country[] = [];
  public selectedcompanies:company[] = [];
  public selectedusers: user[] = [];

  public countries=[];
  public company=[];
  public user=[];

  
  dropdownCountrySettings = {};
  dropdownCompanySettings = {};
  dropdownUserSettings = {};

  constructor(public http: HttpClient, public mainservice: MainServiceService) {
    this.getcountries();
    this.getcompanies();
    this.getusers();
  }
    getusers() {
      this.mainservice.getusers().subscribe(
        data => {
          this.userlist = data;
          this.dropdownUserSettings = {
            singleSelection: false,
            idField: 'userId',
            textField: 'name',
            selectAllText: 'Select All',
            unSelectAllText: 'UnSelect All',
            itemsShowLimit: 3,
            allowSearchFilter: true,
            enableCheckAll: false
          };
        });
    }
    getcompanies() {
      this.mainservice.getcompanies().subscribe(
        data => {
          this.companylist = data;
          this.dropdownCompanySettings = {
            singleSelection: false,
            idField: 'companyId',
            textField: 'name',
            selectAllText: 'Select All',
            unSelectAllText: 'UnSelect All',
            itemsShowLimit: 3,
            allowSearchFilter: true,
            enableCheckAll: false
          };
        });
    }
    getcountries() {
    this.mainservice.getcountries().subscribe(
      data => {
        this.countrylist = data;
        //if (this.countrylist.length > 0) {
        //  for (var i = 0; i < this.countrylist.length; i++) {
        //    this.countrylist[i].item_id = this.countrylist[i].id;
        //    this.countrylist[i].item_text = this.countrylist[i].countryName;
        //    delete this.countrylist[i].id;
        //    delete this.countrylist[i].countryName;
        //  }
        //}
        this.dropdownCountrySettings = {
          singleSelection: false,
          idField: 'id',
          textField: 'countryName',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 3,
          allowSearchFilter: true,
          enableCheckAll: false
        };
      });
    
    }

  ngOnInit(): void {
  }
  onCountrySelect(item: any) {

    this.countries = [];
    for (var i = 0; i < this.selectedcountries.length; i++) {
      this.countries.push(this.selectedcountries[i].id);
    }
    this.loadCompanyfromCountry(this.countries);
    this.loadmaintable();
  }
  onCountryDeSelect() {
    this.countries = []
    for (var i = 0; i < this.selectedcountries.length; i++) {
      this.countries.push(this.selectedcountries[i].id);
    }
    if (this.countries.length > 0) {
      this.loadCompanyfromCountry(this.countries);
      this.loadmaintable();
    }
    else {
      this.getcompanies();
      this.loadmaintable();
    }
    
  }

  onCompanySelect(item: any) {
    this.company = [];
    for (var i = 0; i < this.selectedcompanies.length; i++) {
      this.company.push(this.selectedcompanies[i].companyId);
    }
    this.loadUserfromCompanies(this.company);
    this.loadmaintable();
  }
  onCompanyDeSelect() {
    this.company = []
    for (var i = 0; i < this.selectedcompanies.length; i++) {
      this.company.push(this.selectedcompanies[i].companyId);
    }
    if (this.company.length > 0) {
      this.loadUserfromCompanies(this.company);
      this.loadmaintable();
    }
    else {
      this.getusers();
      this.loadmaintable();
    }
  }

  onUserSelect(item: any) {
    this.user = [];
    for (var i = 0; i < this.selectedusers.length; i++) {
      this.user.push(this.selectedusers[i].userId);
    }
    this.loadmaintable();
  }
  onUserDeSelect() {
    this.user = []
    for (var i = 0; i < this.selectedusers.length; i++) {
      this.user.push(this.selectedusers[i].userId);
    }
    if (this.user.length > 0) {
      this.loadmaintable();
    }
    else {
      this.loadmaintable();
    }
  }

  loadCompanyfromCountry(c: any) {
    return this.mainservice.getcompanyFromCountry(c).subscribe(
      data => this.companylist = data);
  }
  loadUserfromCompanies(c: any) {
    return this.mainservice.getuserFromCompany(c).subscribe(
      data => this.userlist = data);
  }
  loadmaintable() {
    var result :any=[];
    if (
      this.countries.length > 0 ||
      this.company.length > 0 ||
      this.user.length > 0) {
      return this.mainservice.getmainDataTable(this.countries, this.company, this.user).subscribe(
        data => {
          if (this.countries.length == 0 &&
            this.company.length == 0 &&
            this.user.length > 0) {
            
             result = data.reduce((unique, o) => {
              if (!unique.some(obj => obj.companyName === o.companyName)) {
                unique.push(o);
              }
              return unique;
            }, []);
            this.finalResult = result;
          }
          else {
            this.finalResult = data;
          }
          
        });
    }
    else {
      this.finalResult = [];
    }

  }


  //for paging
  next() {
    this.first = this.first + this.rows;
  }
  prev() {
    this.first = this.first - this.rows;
  }
  reset() {
    this.first = 0;
  }

  isLastPage(): boolean {
    return this.finalResult ? this.first === (this.finalResult.length - this.rows) : true;
  }
  isFirstPage(): boolean {
    return this.finalResult ? this.first === 0 : true;
  }
  //
}

import { city } from "./City";
import { country } from "./Country";
import { user } from "./User";

export interface MainDataTable {
  companyName: string;
  countryName: Array<country>;
  cityName: Array<city>;
  user: Array<user>;
}

declare module server {
	interface company {
		companyId: number;
		name: string;
		typeId?: number;
		address: string;
		city: string;
		country: string;
		phone: string;
		emailId: string;
		website: string;
		howComeToKnow: string;
		others: string;
		status: string;
		created?: Date;
		modified?: Date;
		countryID: number;
		cityID: number;
	}
}

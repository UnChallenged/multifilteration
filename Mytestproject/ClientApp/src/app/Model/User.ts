export interface user {
		userId: number;
		companyId?: number;
		loginId: string;
		password: string;
		name: string;
		phone: string;
		emailId: string;
		role: string;
		status: string;
		created?: Date;
		modified?: Date;
  accessed?: Date;
	}


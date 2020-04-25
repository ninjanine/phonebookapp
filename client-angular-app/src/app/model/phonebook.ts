
export interface PhoneEntry {
    name : string;
    phoneNumber: string;
}

export interface PhoneBook {
    id?: string;
    name: string;
    entries: [PhoneEntry];
}


export interface User{
    id: number;
    sub: string;
    role: string;
    email: string;
    allowNotifications: boolean;
}

export class NewUser{
    sub: string;
    //role: string;
    email: string;
}

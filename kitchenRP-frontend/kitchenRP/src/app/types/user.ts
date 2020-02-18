export interface User{
    id: number;
    sub: string;
    role: string;
    email: string;
    allowNotifications: boolean;
}

export class NewUser{
    uid: string;
    //role: string;
    email: string;
}

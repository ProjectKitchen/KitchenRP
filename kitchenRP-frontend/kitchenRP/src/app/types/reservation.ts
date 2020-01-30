import {User} from "./user";
import {Resource} from "./resource";

export interface Reservation{
    id: number;
    //date: any;
    //duration: any;
    startTime: string;
    endTime: string;
    userId: number;
    resourceId: number;
    status: string;
    owner: User;
    reservedResource: Resource;
}

export class NewReservation{
    startTime: string;
    endTime: string;
    userId: number;
    resourceId: number;
    allowNotifications: boolean;
}

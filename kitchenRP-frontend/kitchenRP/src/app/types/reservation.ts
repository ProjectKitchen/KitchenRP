import {User} from "./user";
import {Resource} from "./resource";
import {Status} from "./status";

export interface Reservation{
    id: number;
    //date: any;
    //duration: any;
    startTime: string;
    endTime: string;
    userId: number;
    resourceId: number;
    status: Status;
    statuses: string;
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

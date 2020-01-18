export interface Reservation{
    id: number;
    //date: any;
    //duration: any;
    startTime: any;
    endTime: any;
    userId: number;
    resourceId: number;
    status: string;
}

export class NewReservation{
    startTime: any;
    endTime: any;
    userId: number;
    resourceId: number;
    allowNotifications: boolean;
}

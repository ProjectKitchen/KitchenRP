export interface Reservation{
    id: number;
    //date: any;
    //duration: any;
    startTime: string;
    endTime: string;
    userId: number;
    resourceId: number;
    status: string;
}

export class NewReservation{
    startTime: string;
    endTime: string;
    userId: number;
    resourceId: number;
    allowNotifications: boolean;
}

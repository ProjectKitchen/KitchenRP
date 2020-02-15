export interface Restriction{
    id: number;
    dateFrom: string;
    dateTo: string; 
    ignoreYear: boolean;
    resourceId: number;
    displayError: string;
    restictionData: RestrictionData;
}

export interface NewRestriction{
    dateFrom: string;
    dateTo: string;
    ignoreYear: boolean;
    resourceId: number;
    displayError: string;
    restrictionData: RestrictionData | null;
}

export interface RestrictionData{
    maxUsagesPerMonthHours: number;
    maxUsagesPerWeekCount: number;
}

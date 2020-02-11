export interface Restriction{
    id: number;
    dateFrom: string; // date?
    dateTo: string; // date?
    resource: string;
    ignoreYear: boolean;
    resourceId: number;
    displayError: string;
}
// ^
// | Combine???
// v
export interface RestrictionData{
    maxUsagesPerMonthHours: number;
    maxUsagesPerWeekCount: number;
    restrictionId: number;
}

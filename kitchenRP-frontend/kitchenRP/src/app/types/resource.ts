export interface Resource{
    id: number;
    displayName: string;
    metaData: any;
    description: string | null;
    resourceType: any;
}

export class NewResource{
    displayName: string;
    metaData: any;
    description: string;
    resourceTypeName: string;
}

export interface ResourceType{
    type: string;
    displayName: string;
}

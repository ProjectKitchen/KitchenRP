export interface UserAuth {
    username: string;
    password: string;
}

export interface Token {
    refreshToken: string;
    accessToken: string;
    iat: string;
}

export interface Jwt {
    header: any;
    body: any;
    expires: Date;
}

export function isToken(value: any): value is Token {
    return value.token !== undefined
        && value.expires !== undefined;
}

export function isUserAuth(value: any): value is UserAuth {
    return value === undefined
        || ( value.username !== undefined
            && value.password !== undefined);
}

interface IUser {
    id: string,
    email: string,
    username: string,
    dateCreated: Date
}

interface IUsers {
    users: IUser[]
}

export type {
    IUser,
    IUsers
}
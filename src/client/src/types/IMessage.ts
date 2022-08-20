interface IMessage {
    id: string,
    body: string,
    dateCreated: Date,
    username: string
}

interface IMessages {
    messages: IMessage[]
}

export type {
    IMessage,
    IMessages
}
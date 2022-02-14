export class MessageDTO {
    data: string;
    date: Date;
    type: number;
    groupId: number;
    messageId: number;
    userId: number;

    constructor(DATA: string, DATE: Date, TYPE: number, GROUP_ID: number, MESSAGE_ID: number, USER_ID: number){
        this.data = DATA;
        this.date = DATE;
        this.type = TYPE;
        this.groupId = GROUP_ID;
        this.messageId = MESSAGE_ID;
        this.userId = USER_ID;
    }
}
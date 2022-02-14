import { MessageDTO } from "./MessageDTO";

export class GroupDTO {
    id: number;
    name: string;
    description: string;
    lastMessage: MessageDTO;

    constructor(ID: number, NAME: string, DESC: string, MSG: MessageDTO){
        this.id = ID;
        this.name = NAME;
        this.description = DESC;
        this.lastMessage = MSG;
    }
}
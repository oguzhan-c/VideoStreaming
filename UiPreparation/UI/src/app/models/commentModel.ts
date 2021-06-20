export interface CommentModel {
    id : number;
    postedByUserId : number;
    responseByUserId : number;
    likeId : number;
    dislakeId : number;
    commentBody : string;
    date : string;
}
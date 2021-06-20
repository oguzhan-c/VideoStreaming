export interface VideoModel {
  id: number;
  userId: number;
  channelId: number;
  trendId: number;
  description: string;
  views: number;
  duration: number;
  videoPath: string;
  thumbnailPath: string;
  date: string;
  updateDate: string;
}

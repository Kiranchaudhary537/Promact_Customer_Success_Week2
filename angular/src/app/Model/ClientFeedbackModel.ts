export interface ClientFeedback {
  id?: string;
  projectId: string;
  dateReceived: string;
  feedbackType: FeedbackType;
  detailedFeedback: string;
  actionTaken: boolean;
  closureDate?: string;
}

export enum FeedbackType {
  Positive="Positive",
  Negative="Negative"
}

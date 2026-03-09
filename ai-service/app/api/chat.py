from fastapi import APIRouter
from app.services.ai_service import get_ai_response
from pydantic import BaseModel

router = APIRouter(prefix="/chat")

class ChatRequest(BaseModel):
    message: str
    history: list = []

@router.post("/ask")
def ask(request: ChatRequest):
    answer = get_ai_response(request.message, request.history)
    return {
        "question": request.message,
        "answer": answer
    }
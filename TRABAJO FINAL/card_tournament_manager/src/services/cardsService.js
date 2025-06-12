import apiClient from "./apiClient";

export const getCards = async () => {
	const response = await apiClient.get(`/cartas/listar-cartas`);
	return response.data;
};

export const createDeck = async (deckData) => {
	console.log(deckData, "deckdata en el service");
	const response = await apiClient.post(`/cartas/crear-mazo`, deckData);

	return response.data;
};

export const getDecksByUserId = async (userId) => {
	const response = await apiClient.get(`/cartas/Mazos/${userId}`);
	return response.data;
};

export const getCardDetailsByIds = async (cardIds) => {
	const response = await apiClient.post(`/cartas/details`, { ids: cardIds });
	return response.data;
};
export const getCardsOfDeck = async (mazoId) => {
	const response = await apiClient.get(`/cartas/Mazos/${mazoId}/Cartas`);
	return response.data;
};

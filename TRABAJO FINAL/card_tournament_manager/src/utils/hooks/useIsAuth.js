import { useSelector } from "react-redux";

const useIsAuth = () => {
	const token = useSelector((state) => state.auth.token);
	return !!token;
};
export default useIsAuth;

export const API_ENDPOINTS = {
  AUTH: {
    REGISTER: '/auth/register',
    LOGIN: '/auth/login',
    GET_USER_BY_ID: '/auth/user',  
    UPDATE_USER: '/auth/update-user',
    FORGOT_PASSWORD: '/auth/forgot-password',
    RESET_PASSWORD: '/auth/reset-password',
    CHANGE_PASSWORD: '/auth/change-password',
  },

  // You can add other endpoint groups if needed, e.g.:
  USER: {
    GET_ALL: '/user/all',
    DELETE: '/user/delete', // etc.
  }
};

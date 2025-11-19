const API_BASE = '/api';

export async function getAdsList() {
  const resp = await fetch(`${API_BASE}/ads`);
  if (!resp.ok) throw new Error(`Failed to fetch ads (${resp.status})`);
  return resp.json();
}

export async function getAdById(id) {
  const resp = await fetch(`${API_BASE}/ads/${id}`);
  if (!resp.ok) throw new Error(`Failed to fetch ad (${resp.status})`);
  return resp.json();
}
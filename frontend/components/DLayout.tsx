"use client";
import { useRouter } from 'next/navigation';
import React from 'react';

const DLayout = ({ children }: { children: React.ReactNode }) => {
  const router = useRouter();

  return (
    <div className="flex min-h-screen">
      <div className="w-60 bg-white text-white flex flex-col p-4 sticky top-0 h-3/4 items-center mt-6 ml-5 border rounded-lg">
        <h2 className="text-xl font-bold mb-4 text-black">Menu</h2>
        <nav className="flex flex-col space-y-2">
          <button
            className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/users')}
          >
            Users
          </button>
          <button
            className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/playlist')}
          >
            PlayLists
          </button>
          <button
            className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/songs')}
          >
            Songs
          </button>
          <button
            className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/playlistsong')}
          >
            Playlists Songs
          </button>
          <button
            className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/recent')}
          >
            Recently Played
          </button>
          <button
            className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/sharedlist')}
          >
            Shared Playlists
          </button>
          <button
            className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/shre')}
          >
            Shuffle and Repeat
          </button>
          <button
            className="bg-[#1679AB] hover:bg-[#074173] text-white py-2 px-4 rounded-lg"
            onClick={() => router.push('/auth/reset-request')}
          >
            Forgot Passowrd ?
          </button>
        </nav>
      </div>

      {/* Content Area */}
      <div className="flex-grow bg-gray-200 p-6">{children}</div>
    </div>
  );
};

export default DLayout;